using System.Net.Mime;
using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[Controller]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(DataContext dataContext, IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponseDto<ProductDto>))]
    public async Task<ActionResult<PaginatedResponseDto<ProductDto>>> GetAll([FromQuery] ProductFilterDto query)
    {
        var result = await _productService.GetAll(query);

        return Ok(result);
    }

    [HttpGet("{uuid:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<ProductDto>> Get(Guid uuid)
    {
        var product = await _productService.Show(uuid);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult> Create([FromBody] ProductCreateDto data)
    {
        var result = await _productService.Create(data);

        return Ok(result);
    }
}
