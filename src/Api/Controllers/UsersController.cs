using Api.Extensions;
using Application.DTOs.Patients;
using Application.DTOs.Users;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return this.ToActionResult(await _userService.GetByIdAsync(id));
    }

    [HttpPost("Patient")]
    public async Task<IActionResult> CreatePatient([FromBody] PatientRequest request)
    {
        return this.ToActionResult(await _userService.CreateAsync(request));
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        return this.ToActionResult(await _userService.LoginAsync(request));
    }
}