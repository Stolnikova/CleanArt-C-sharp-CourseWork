using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Mappers;
using CleaningCrm.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleaningCrm.Controllers;

[ApiController]
[Route("api/v1/companies/{companyId}/contacts")]
[Authorize]
public class ContactPersonsController : ControllerBase
{
    private readonly IContactPersonService _service;
    private readonly ICompanyService _companyService;
    private readonly ContactPersonMapper _mapper;

    public ContactPersonsController(
        IContactPersonService service,
        ICompanyService companyService,
        ContactPersonMapper mapper)
    {
        _service = service;
        _companyService = companyService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactPersonResponse>>> GetByCompany(
        [FromRoute] int companyId)
    {
        Company? company = await _companyService.GetByIdAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        IEnumerable<ContactPerson> contacts = await _service.GetByCompanyIdAsync(companyId);
        return Ok(_mapper.ToResponseList(contacts));
    }

    [HttpPost]
    public async Task<ActionResult<ContactPersonResponse>> Create(
        [FromRoute] int companyId,
        [FromBody] CreateContactPersonRequest request)
    {
        Company? company = await _companyService.GetByIdAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        ContactPerson entity = _mapper.ToEntity(request, companyId);
        ContactPerson created = await _service.CreateAsync(entity);
        return Created(string.Empty, _mapper.ToResponse(created));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ContactPersonResponse>> Update(
        [FromRoute] int companyId,
        [FromRoute] int id,
        [FromBody] UpdateContactPersonRequest request)
    {
        Company? company = await _companyService.GetByIdAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        ContactPerson? existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        ContactPerson entity = _mapper.ToEntity(request, id, companyId);
        ContactPerson updated = await _service.UpdateAsync(entity);
        return Ok(_mapper.ToResponse(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int companyId,
        [FromRoute] int id)
    {
        Company? company = await _companyService.GetByIdAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        ContactPerson? existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(id);
        return NoContent();
    }
}