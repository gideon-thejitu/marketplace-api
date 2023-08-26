using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Controllers;

[Controller]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly DataContext _dataContext;

    public ProductsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet("{uuid:guid}")]
    public async Task<ActionResult<ProductDto>> Get(Guid uuid)
    {
        var product = await _dataContext.Products.FirstOrDefaultAsync(t => t.ProductId == uuid);

        if (product is null)
        {
            return NotFound();
        }

        return new ProductDto()
        {
            ProductId = product.ProductId,
            CategoryId = product.Category.CategoryId,
            ProductStatusId = product.ProductStatus.ProductStatusId,
            Description = product.Description,
            BasePrice = Decimal.Zero
        };
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProductDto data)
    {
        return Ok(data);
    }
}
