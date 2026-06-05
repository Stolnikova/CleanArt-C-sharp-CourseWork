using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Mappers;
using CleaningCrm.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleaningCrm.Controllers;

[Authorize(Roles = "Admin")]
[ApiController] 
[Route("api/v1/services")]

public class ServicesController : ControllerBase
{
    private readonly IServiceCatalogService _service;
    private readonly ServiceItemMapper _mapper;

    public ServicesController(IServiceCatalogService service, ServiceItemMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<ServiceItem> items = await _service.GetAllAsync();
        IEnumerable<ServiceItemResponse> response = _mapper.ToResponseList(items);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        ServiceItem item = await _service.GetByIdAsync(id);
        ServiceItemResponse response = _mapper.ToResponse(item);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceItemRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        ServiceItem entity = _mapper.ToEntity(request);
        ServiceItem created = await _service.CreateAsync(entity);
        ServiceItemResponse response = _mapper.ToResponse(created);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateServiceItemRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        ServiceItem entity = _mapper.ToEntity(request, id);
        ServiceItem updated = await _service.UpdateAsync(entity);
        ServiceItemResponse response = _mapper.ToResponse(updated);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

}