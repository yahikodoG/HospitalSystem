using Api.Extensions;
using Application.DTOs.Suppliers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _supplierService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return this.ToActionResult(await _supplierService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SupplierRequest request)
    {
        return this.ToActionResult(await _supplierService.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] SupplierRequest request, int id)
    {
        return this.ToActionResult(await _supplierService.UpdateAsync(request, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return this.ToActionResult(await _supplierService.DeleteAsync(id));
    }
}