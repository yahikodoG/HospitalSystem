using Api.Extensions;
using Application.DTOs.Rooms;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _roomService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return this.ToActionResult(await _roomService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoomRequest request)
    {
        return this.ToActionResult(await _roomService.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoomRequest request)
    {
        return this.ToActionResult(await _roomService.UpdateAsync(request, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return this.ToActionResult(await _roomService.DeleteAsync(id));
    }
}