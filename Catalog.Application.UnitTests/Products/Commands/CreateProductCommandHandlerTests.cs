using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.Profiles;
using Catalog.Application.UnitTests.Mocks;
using Catalog.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.Application.UnitTests.Products.Commands;

public class CreateProductCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly ILoggerFactory _loggerFactory;

    public CreateProductCommandHandlerTests()
    {
        _mockProductRepository = RepositoryMocks.GetProductRepository();

        _loggerFactory = LoggerFactory.Create(builder => { });

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }, _loggerFactory);

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_ReturnsCreatedProductId()
    {
        //Arrange
        var createdProductId = Guid.NewGuid();

        _mockProductRepository.Setup(repo => repo
            .AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(
                (Product product) =>
                {
                    product.Id = createdProductId;
                    return product;
                });

        var _handler = new CreateProductCommandHandler(_mapper, _mockProductRepository.Object);
        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand() { 
            Name = "FakeProduct", 
            Price = 100.00M,
            BrandId = brandId, 
            CategoryId = categoryId, 
            Stocks = { 
                new() { Size = "L", InStore = 0, Online = 1 } 
            } 
        };

        //Act
        var result = await _handler.Handle(productCommand, CancellationToken.None);
        //Assert
        result.Should().Be(createdProductId);
    }
    
    [Fact]
    public async Task Handle_WhenCommandIsValid_CallsAddAsyncOnce()
    {
        //Arrange
        var _handler = new CreateProductCommandHandler(_mapper, _mockProductRepository.Object);
        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeProduct",
            Price = 100.00M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await _handler.Handle(productCommand, CancellationToken.None);
        //Assert
        _mockProductRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_MapsCommandToProductCorrectly()
    {
        //Arrange
        var _handler = new CreateProductCommandHandler(_mapper, _mockProductRepository.Object);
        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeProduct",
            Price = 100.00M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await _handler.Handle(productCommand, CancellationToken.None);
        //Assert
        _mockProductRepository.Verify(r => r.AddAsync(
            It.Is<Product>(product =>
                product.Name == productCommand.Name &&
                product.Price == productCommand.Price &&
                product.BrandId == productCommand.BrandId &&
                product.CategoryId == productCommand.CategoryId &&
                product.Stocks.Count == productCommand.Stocks.Count)
            ), Times.Once);
    }
    [Fact]
    public async Task Handle_WhenRepositoryFails_ThrowsException()
    {
        var _handler = new CreateProductCommandHandler(_mapper, _mockProductRepository.Object);
        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeProduct",
            Price = 100.00M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        _mockProductRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Product>()))
            .ThrowsAsync(new Exception("Database error"));

        var act = async () => await _handler.Handle(productCommand, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }
}
