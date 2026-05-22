using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleaningCrm.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        AuthResponse response = await _service.LoginAsync(request.Login, request.Password);
        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        AuthResponse response = await _service.RefreshAsync(request.RefreshToken);
        return Ok(response);
    }
}