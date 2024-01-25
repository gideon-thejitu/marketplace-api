using System.Net.Mime;
using marketplace_api.Dto;
using marketplace_api.Infrastructure.Filters;
using marketplace_api.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/product-statuses")]
public class ProductStatusesController : ControllerBase
{
    public readonly IProductStatusService _productStatusService;

    public ProductStatusesController(IProductStatusService productStatusService)
    {
        _productStatusService = productStatusService;
    }

    [HttpGet("productStatusId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductStatusDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<ProductStatusDto>> Show(Guid productStatusId)
    {
        var status = await _productStatusService.Show(productStatusId);

        if (status is null)
        {
            return NotFound($"Product Status with id = {productStatusId} not found");
        }

        return Ok(status);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponseDto<ProductStatusDto>))]
    public async Task<ActionResult<PaginatedResponseDto<ProductStatusDto>>> GetAll([FromQuery] ProductStatusFilterDto query)
    {
        var result = await _productStatusService.GetAll(query);

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    [IsAuthorizedFor("product_status", "create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(JsonResult))]
    public async Task<ActionResult<ProductStatusDto>> Create([FromBody] ProductStatusCreateDto data)
    {
        var result = await _productStatusService.Create(data);

        return CreatedAtAction(nameof(Show), new { categoryId = result.ProductStatusId }, result);
    }
}
