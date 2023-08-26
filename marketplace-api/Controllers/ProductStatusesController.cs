using System.Net.Mime;
using marketplace_api.Dto;
using marketplace_api.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[Route("api/product-statuses")]
[ApiController]
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
    [Consumes(MediaTypeNames.Application.Json)]
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
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<PaginatedResponseDto<ProductStatusDto>>> GetAll([FromQuery] ProductStatusFilterDto query)
    {
        var result = await _productStatusService.GetAll(query);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ProductStatusDto>> Create([FromBody] ProductStatusCreateDto data)
    {
        var result = await _productStatusService.Create(data);

        return CreatedAtAction(nameof(Show), new { categoryId = result.ProductStatusId }, result);
    }
}
