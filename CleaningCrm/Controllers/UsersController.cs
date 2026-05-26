using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Mappers;
using CleaningCrm.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleaningCrm.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly UserMapper _mapper;

    public UsersController(IUserService service, UserMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{login}")]
    public async Task<IActionResult> GetByLogin(string login)
    {
        User user = await _service.GetByLoginAsync(login);
        UserResponse response = _mapper.ToResponse(user);
        return Ok(response);
    }
}

