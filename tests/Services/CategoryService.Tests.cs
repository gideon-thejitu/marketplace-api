using FluentAssertions;
using marketplace_api.Dto;
using marketplace_api.Services.CategoryService;
using Moq;

namespace tests.Services;

public class CategoryServiceTest
{
    [Fact]
    async void TestGetAllReturnsPaginatedCategories()
    {
        var mock = new Mock<ICategoryService>();

        var result = await mock.Object.GetAll(new CategoryFilterDto()
        {
            Limit = 1,
            Page = 1
        });

        result?.Limit.Should().Be(1);
        result?.Page.Should().Be(1);
        result?.Results.Count.Should().Be(0);
        result?.Results.Should().BeOfType<PaginatedResponseDto<CategoryDto>>();
    }
}
