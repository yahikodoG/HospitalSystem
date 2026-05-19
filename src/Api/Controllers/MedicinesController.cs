using Api.Extensions;
using Application.DTOs.Medicines;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicinesController : ControllerBase
{
    private readonly IMedicineService _medicineService;

    public MedicinesController(IMedicineService medicineService)
    {
        _medicineService = medicineService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _medicineService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return this.ToActionResult(await _medicineService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicineRequest request)
    {
        return this.ToActionResult(await _medicineService.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] MedicineRequest request, int id)
    {
        return this.ToActionResult(await _medicineService.UpdateAsync(request, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return this.ToActionResult(await _medicineService.DeleteAsync(id));
    }
}