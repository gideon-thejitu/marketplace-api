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
        try
        {
            var price = new ProductPrice()
            {
                BasePrice = data.BasePrice,
                StartDate = DateTime.Now
            };

            return Ok(price);
            
            // var productStatus = await _dataContext.ProductStatuses.Where(t => t.ProductStatusId == data.ProductStatusId)
            //     .FirstOrDefaultAsync();
            //
            // var category = await _dataContext.Categories.Where(t => t.CategoryId == data.CategoryId)
            //     .FirstOrDefaultAsync();
            
            // if (productStatus is not null && category is not null)
            // {
            //     _dataContext.ProductPrices.Add(price);
            //     // var product = new Product()
            //     // {
            //     //     Name = data.Name,
            //     //     Description = data.Description,
            //     //     Category = category,
            //     //     ProductStatus = productStatus,
            //     // };
            //     //
            //     // await _dataContext.SaveChangesAsync();
            //     // return CreatedAtAction(nameof(Get), new { uuid = product.ProductId }, product);   
            // }
            // else
            // {
            //     throw new ArgumentException("somethis ");
            // }
            // await _dataContext.SaveChangesAsync();

            return Ok("oka");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
