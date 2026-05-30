using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Mappers;
using CleaningCrm.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleaningCrm.Controllers;

[ApiController]
[Route("api/v1/companies/{companyId}/contacts/{contactPersonId}/addresses")]
[Authorize]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _service;
    private readonly IContactPersonService _contactPersonService;
    private readonly AddressMapper _mapper;

    public AddressesController(
        IAddressService service,
        IContactPersonService contactPersonService,
        AddressMapper mapper)
    {
        _service = service;
        _contactPersonService = contactPersonService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressResponse>>> GetByContactPerson(
        [FromRoute] int contactPersonId)
    {
        ContactPerson? contactPerson = await _contactPersonService.GetByIdAsync(contactPersonId);
        if (contactPerson == null)
        {
            return NotFound();
        }

        IEnumerable<Address> addresses = await _service.GetByContactPersonIdAsync(contactPersonId);
        return Ok(_mapper.ToResponseList(addresses));
    }

    [HttpPost]
    public async Task<ActionResult<AddressResponse>> Create(
        [FromRoute] int contactPersonId,
        [FromBody] CreateAddressRequest request)
    {
        ContactPerson? contactPerson = await _contactPersonService.GetByIdAsync(contactPersonId);
        if (contactPerson == null)
        {
            return NotFound();
        }

        Address entity = _mapper.ToEntity(request, contactPersonId);
        Address created = await _service.CreateAsync(entity);
        return Created(string.Empty, _mapper.ToResponse(created));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AddressResponse>> Update(
        [FromRoute] int contactPersonId,
        [FromRoute] int id,
        [FromBody] UpdateAddressRequest request)
    {
        ContactPerson? contactPerson = await _contactPersonService.GetByIdAsync(contactPersonId);
        if (contactPerson == null)
        {
            return NotFound();
        }

        Address? existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        Address entity = _mapper.ToEntity(request, id, contactPersonId);
        Address updated = await _service.UpdateAsync(entity);
        return Ok(_mapper.ToResponse(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int contactPersonId,
        [FromRoute] int id)
    {
        ContactPerson? contactPerson = await _contactPersonService.GetByIdAsync(contactPersonId);
        if (contactPerson == null)
        {
            return NotFound();
        }

        Address? existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(id);
        return NoContent();
    }
}