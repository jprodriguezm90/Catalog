using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Exceptions;
using Catalog.Application.Features.Products.Queries.GetProductDetail;
using Catalog.Application.Profiles;
using Catalog.Application.UnitTests.Mocks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;

namespace Catalog.Application.UnitTests.Products.Queries;

public class GetProductDetailQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly ILoggerFactory _loggerFactory;

    public GetProductDetailQueryHandlerTests()
    {
        _mockProductRepository = RepositoryMocks.GetProductRepository();
        
        _loggerFactory = LoggerFactory.Create(builder => { });

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }, _loggerFactory);

        _mapper = configurationProvider.CreateMapper();
    }

    [InlineData("CF198219-DA46-4264-B5ED-7565A56D26FE")]
    [InlineData("59842595-1129-439E-A84F-DFF86C261A3C")]
    [InlineData("99199924-E42C-4DCF-A2EA-DC482BC3C7BE")]
    [InlineData("4DB2C7D5-FC02-4BC4-A245-9F0A5396083A")]
    [InlineData("0064DB26-87A7-4DA2-AA13-C72096303B10")]
    [InlineData("8A8038F9-9DF9-4A7B-A76E-414AE1312827")]
    [Theory]
    public async Task Handle_WhenProductExists_ReturnsProductDetail(string id)
    {
        //Arrange
        var productId = Guid.Parse(id);
        var handler = new GetProductDetailHandler(_mapper, _mockProductRepository.Object);
        var expectedProduct = TestSeedData.GetProducts().First(p => p.Id == productId);

        //Act
        var result = await handler.Handle(new GetProductDetailQuery() { ProductId = productId }, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();

        result.Name.Should().Be(expectedProduct.Name);
        result.Price.Should().Be(expectedProduct.Price);
        result.BrandName.Should().Be(expectedProduct.Brand.Name);
        result.CategoryName.Should().Be(expectedProduct.Category.Name);

        result.Stocks.Should().HaveCount(expectedProduct.Stocks.Count);
        
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var handler = new GetProductDetailHandler(_mapper, _mockProductRepository.Object);
        var productId = Guid.Parse("00000000-0000-0000-0000-000000000000");

        // Act
        var act = async () => await handler.Handle(new GetProductDetailQuery() { ProductId = productId }, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
