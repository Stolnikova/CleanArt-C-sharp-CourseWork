using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Mappers;
using CleaningCrm.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleaningCrm.Controllers;

[ApiController]
[Route("api/v1/companies")]
[Authorize]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _service;
    private readonly CompanyMapper _mapper;

    public CompaniesController(ICompanyService service, CompanyMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyResponse>>> GetAll([FromQuery] string? search)
    {
        IEnumerable<Company> companies;

        if (string.IsNullOrWhiteSpace(search))
        {
            companies = await _service.GetAllAsync();
        }
        else
        {
            companies = await _service.SearchAsync(search);
        }

        return Ok(_mapper.ToResponseList(companies));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyResponse>> GetById(int id)
    {
        Company? company = await _service.GetByIdAsync(id);
        if (company == null)
        {
            return NotFound();
        }
        return Ok(_mapper.ToResponse(company));
    }

    [HttpPost]
    public async Task<ActionResult<CompanyResponse>> Create([FromBody] CreateCompanyRequest request)
    {
        Company entity = _mapper.ToEntity(request);
        Company created = await _service.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.ToResponse(created));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CompanyResponse>> Update(int id, [FromBody] UpdateCompanyRequest request)
    {
        Company? existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }
        Company entity = _mapper.ToEntity(request, id);
        Company updated = await _service.UpdateAsync(entity);
        return Ok(_mapper.ToResponse(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Company? existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }
        await _service.DeleteAsync(id);
        return NoContent();
    }
}