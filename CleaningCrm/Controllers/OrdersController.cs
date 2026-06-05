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
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IDocumentService _documentService;
    private readonly OrderMapper _mapper;

    public OrdersController(IOrderService service, IDocumentService documentService, OrderMapper mapper)
    {
        _service = service;
        _documentService = documentService;
        _mapper = mapper;
    }

    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] string? companyName,
        [FromQuery] string? contactName,
        [FromQuery] string? address,
        [FromQuery] string? serviceName)
    {
        IEnumerable<Order> orders = await _service.GetFilteredAsync(
            from, to, companyName, contactName, address, serviceName);
        return Ok(_mapper.ToResponseList(orders));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Order order = await _service.GetByIdAsync(id);
        OrderResponse response = _mapper.ToResponse(order);
        return Ok(response);
    }

    [HttpGet("{id}/document")]
    public async Task<IActionResult> GetDocument(int id)
    {
        Order order = await _service.GetByIdAsync(id);
        byte[] bytes = _documentService.GenerateAct(order);
        return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"act_{id}.docx");
    }
    
    [HttpGet("{id}/invoice")]
    public async Task<IActionResult> GetInvoice(int id)
    {
        Order order = await _service.GetByIdAsync(id);
        byte[] bytes = _documentService.GenerateInvoice(order);
        return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"invoice_{id}.docx");
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Order entity = _mapper.ToEntity(request);
        Order created = await _service.CreateAsync(entity);
        OrderResponse response = _mapper.ToResponse(created);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateOrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Order entity = _mapper.ToEntity(request, id);
        Order updated = await _service.UpdateAsync(entity);
        OrderResponse response = _mapper.ToResponse(updated);
        return Ok(response);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(int id, ChangeOrderStatusRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _service.ChangeStatusAsync(id, request.Status);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}