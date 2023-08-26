using System.Data.Entity;
using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Models;
using Microsoft.AspNetCore.Mvc;

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
            CategoryId = product.CategoryId,
            ProductStatusId = product.ProductStatusId,
            Description = product.Description
        };
    }

    [HttpPost()]
    public async Task<ActionResult<Product>> Create([FromBody] ProductDto data)
    {
        try
        {
            var product = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                CategoryId = data.CategoryId,
                ProductStatusId = data.ProductStatusId
            };

            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { uuid = product.ProductId }, product);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
