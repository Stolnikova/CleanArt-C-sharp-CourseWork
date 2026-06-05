using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Mappers;
using CleaningCrm.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleaningCrm.Controllers;

[ApiController]
[Route("api/v1/contacts")]
[Authorize(Roles = "Admin")]
public class ContactsController : ControllerBase
{
    private readonly IContactPersonService _service;
    private readonly ContactPersonMapper _mapper;

    public ContactsController(IContactPersonService service, ContactPersonMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactPersonResponse>>> GetAll([FromQuery] string? search)
    {
        IEnumerable<ContactPerson> contacts;

        if (string.IsNullOrWhiteSpace(search))
        {
            contacts = await _service.GetAllAsync();
        }
        else
        {
            contacts = await _service.SearchAsync(search);
        }

        return Ok(_mapper.ToResponseList(contacts));
    }

    [HttpPost]
    public async Task<ActionResult<ContactPersonResponse>> Create(
        [FromBody] CreateContactPersonRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ContactPerson entity = _mapper.ToEntityIndependent(request);
        ContactPerson created = await _service.CreateAsync(entity);
        return Created($"api/v1/contacts/{created.Id}", _mapper.ToResponse(created));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ContactPersonResponse>> Update(
        int id,
        [FromBody] UpdateContactPersonRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ContactPerson? existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        ContactPerson entity = _mapper.ToEntityIndependent(request, id);
        ContactPerson updated = await _service.UpdateAsync(entity);
        return Ok(_mapper.ToResponse(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        ContactPerson? existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(id);
        return NoContent();
    }
}