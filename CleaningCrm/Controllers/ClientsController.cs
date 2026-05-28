using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Mappers;
using CleaningCrm.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleaningCrm.Controllers;

[Authorize] 
[ApiController] 
[Route("api/v1/clients")]

public class ClientsController : ControllerBase
{
    private readonly IClientService _service;
    private readonly ClientMapper _mapper;

    public ClientsController(IClientService service, ClientMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            IEnumerable<Client> clients = await _service.GetAllAsync();
            return Ok(_mapper.ToResponseList(clients));
        }
        IEnumerable<Client> found = await _service.SearchAsync(search);
        return Ok(_mapper.ToResponseList(found));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Client client = await _service.GetByIdAsync(id);
        ClientResponse response = _mapper.ToResponse(client);
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateClientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Client client = _mapper.ToEntity(request);
        Client created = await _service.CreateAsync(client);
        ClientResponse response = _mapper.ToResponse(created);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateClientRequest request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Client client = _mapper.ToEntity(request, id);
        Client created = await _service.UpdateAsync(client);
        ClientResponse response = _mapper.ToResponse(created);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}