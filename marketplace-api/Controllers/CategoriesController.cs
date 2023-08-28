using System.Net.Mime;
using marketplace_api.Dto;
using marketplace_api.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    public readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("categoryId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<CategoryDto>> Show(Guid categoryId)
    {
        var category = await _categoryService.Show(categoryId);

        if (category is null)
        {
            return NotFound($"Category with id = {categoryId} not found");
        }

        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CategoryCreateDto data)
    {
        var result = await _categoryService.Create(data);

        return CreatedAtAction(nameof(Show), new { categoryId = result.CategoryId }, result);
    }
}