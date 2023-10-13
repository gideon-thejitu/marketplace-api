using FluentAssertions;
using marketplace_api.Controllers;
using marketplace_api.Dto;
using marketplace_api.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace tests.Controllers;

public class CategoriesControllerTests
{
    private readonly Mock<ICategoryService> _service;
    public CategoriesControllerTests()
    {
        _service = new Mock<ICategoryService>();
    }

    [Fact]
    async void TestGetAllCategoriesReturnsOk()
    {
        CategoryFilterDto query = new CategoryFilterDto()
        {
            Page = 1,
            Limit = 10
        };

        var controller = new CategoriesController(_service.Object);

        var result = await controller.GetAll(query);

        result.Result.Should().NotBe(null);
        result.Result.Should().BeOfType<OkObjectResult>();
    }
}
