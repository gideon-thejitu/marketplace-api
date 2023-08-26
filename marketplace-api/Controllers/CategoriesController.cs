using System.Net.Mime;
using marketplace_api.Dto;
using marketplace_api.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    public readonly ICategoryService _categorySevice;

    public CategoriesController(ICategoryService categoryService)
    {
        _categorySevice = categoryService;
    }

    [HttpGet("categoryId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<CategoryDto>> Show(Guid categoryId)
    {
        var category = await _categorySevice.Show(categoryId);

        if (category is null)
        {
            return NotFound($"Category with id = {categoryId} not found");
        }

        return Ok(category);
    }
}