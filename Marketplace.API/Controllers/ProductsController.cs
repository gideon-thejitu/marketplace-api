using System.Net.Mime;
using Marketplace.Dto;
using Marketplace.Infrastructure.Data;
using Marketplace.Infrastructure.Filters;
using Marketplace.Services.AuthService;
using Marketplace.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers;

[Controller]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ICurrentUserService _currentUserService;

    public ProductsController(DataContext dataContext, IProductService productService, ICurrentUserService currentUserService)
    {
        _productService = productService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    [IsAuthorizedFor("product", "read")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponseDto<ProductDto>))]
    public async Task<ActionResult<PaginatedResponseDto<ProductDto>>> GetAll([FromQuery] ProductFilterDto query)
    {
        var result = await _productService.GetAll(query);

        return Ok(result);
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult<ProductDto>> Get(Guid productId)
    {
        var product = await _productService.Show(productId);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    [HttpPost]
    [Authorize]
    [IsAuthorizedFor("product", "create")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(JsonResult))]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductCreateDto data)
    {
        var user = await _currentUserService.GetCurrentUser();
        data.CreatedById = user.Id;
        var result = await _productService.Create(data);

        return Ok(result);
    }

    [HttpPut("{productId:guid}")]
    [Authorize]
    [IsAuthorizedFor("product", "update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(JsonResult))]
    public async Task<ActionResult<ProductDto>> Update([FromBody] ProductDto data, Guid productId)
    {
        var result = await _productService.Update(productId, data);

        if (result is null)
        {
            return NotFound($"Product with ProductId={productId} not found");
        }

        return Ok(result);
    }

    [HttpDelete("{productId:guid}")]
    [Authorize]
    [IsAuthorizedFor("product", "delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(JsonResult))]
    public async Task<ActionResult> Destroy(Guid productId)
    { 
        var product = await _productService.Exists(productId);

        if (!product)
        {
            return NotFound($"Product with id={productId.ToString()} does not exist!");
        }

        await _productService.Destroy(productId);

        return NoContent();
    }
}
