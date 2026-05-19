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
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        return this.ToActionResult(await _roomService.GetAllAsync(search, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return this.ToActionResult(await _roomService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] RoomRequest request,
        CancellationToken cancellationToken)
    {
        return this.ToActionResult(await _roomService.CreateAsync(request, cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] RoomRequest request, int id)
    {
        return this.ToActionResult(await _roomService.UpdateAsync(request, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return this.ToActionResult(await _roomService.DeleteAsync(id));
    }
}