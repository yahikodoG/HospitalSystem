using Api.Extensions;
using Application.DTOs.Services;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceService __serviceService;

    public ServicesController(IServiceService serviceService)
    {
        __serviceService = serviceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await __serviceService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return this.ToActionResult(await __serviceService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServiceRequest request)
    {
        return this.ToActionResult(await __serviceService.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] ServiceRequest request, int id)
    {
        return this.ToActionResult(await __serviceService.UpdateAsync(request, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return this.ToActionResult(await __serviceService.DeleteAsync(id));
    }
}