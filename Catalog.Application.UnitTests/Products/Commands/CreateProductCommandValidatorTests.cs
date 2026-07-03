using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.UnitTests.Mocks;
using FluentAssertions;
using Moq;

namespace Catalog.Application.UnitTests.Products.Commands;

public class CreateProductCommandValidatorTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    public CreateProductCommandValidatorTests()
    {
        _mockProductRepository = RepositoryMocks.GetProductRepository();
    }

    [Fact]
    public async Task Validate_WhenCommandIsValid_ReturnsNoValidationErrors()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "Valid Product",
            Price = 100.00M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_WhenNameIsEmpty_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "",
            Price = 100.00M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);


        //Assert
        result.IsValid.Should().BeFalse();

        var expectedErrorMessage = $"Name is required.";
        var expectedPropertyName = $"Name";
        result.Errors.Find(f => f.ErrorMessage == expectedErrorMessage && f.PropertyName == expectedPropertyName).Should().NotBeNull();
    }

    [Fact]
    public async Task Validate_WhenNameLongerThan50_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
            Price = 100.00M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);


        //Assert
        result.IsValid.Should().BeFalse();

        var expectedPropertyName = $"Name";
        var expectedErrorMessage = $"Name must not exceed 50 characters.";
        result.Errors.Find(f => f.ErrorMessage == expectedErrorMessage && f.PropertyName == expectedPropertyName).Should().NotBeNull();
    }

    [Fact]
    public async Task Validate_WhenStockIsNull_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeName",
            Price = 100.0M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = null!
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);

        //Assert
        result.IsValid.Should().BeFalse();

        var expectedPropertyName = $"Stocks";
        result.Errors.Find(f => f.PropertyName == expectedPropertyName).Should().NotBeNull();
    }

    [Fact]
    public async Task Validate_WhenStockSizeIsEmpty_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeName",
            Price = 100.0M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);

        //Assert
        result.IsValid.Should().BeFalse();

        var expectedPropertyName = $"Stocks[0].Size";
        result.Errors.Find(f => f.PropertyName == expectedPropertyName).Should().NotBeNull();
    }

    [Fact]
    public async Task Validate_WhenStockInStoreIsNegative_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeName",
            Price = 100.0M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = -50, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);

        //Assert
        result.IsValid.Should().BeFalse();

        var expectedPropertyName = $"Stocks[0].InStore";
        result.Errors.Find(f => f.PropertyName == expectedPropertyName).Should().NotBeNull();
    }

    [Fact]
    public async Task Validate_WhenStockOnlineIsNegative_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeName",
            Price = 100.0M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = -1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);

        //Assert
        result.IsValid.Should().BeFalse();

        var expectedPropertyName = $"Stocks[0].Online";
        result.Errors.Find(f => f.PropertyName == expectedPropertyName).Should().NotBeNull();
    }

    [Fact]
    public async Task Validate_WhenPriceIsZero_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeName",
            Price = 0.0M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);

        //Assert
        result.IsValid.Should().BeFalse();

        var expectedPropertyName = $"Price";
        result.Errors.Find(f => f.PropertyName == expectedPropertyName).Should().NotBeNull();
    }

    [Fact]
    public async Task Validate_WhenBrandIdIsEmpty_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeName",
            Price = 100.0M,
            BrandId = Guid.Empty,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);

        //Assert
        result.IsValid.Should().BeFalse();

        var expectedPropertyName = $"BrandId";
        result.Errors.Find(f => f.PropertyName == expectedPropertyName).Should().NotBeNull();
    }

    [Fact]
    public async Task Validate_WhenCategoryIdIsEmpty_ReturnsValidationError()
    {
        //Arrange
        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.pantsGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "FakeName",
            Price = 100.00M,
            BrandId = brandId,
            CategoryId = Guid.Empty,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);

        //Assert
        result.IsValid.Should().BeFalse();

        var expectedPropertyName = $"CategoryId";
        result.Errors.Find(f => f.PropertyName == expectedPropertyName).Should().NotBeNull();

    }

    [Fact]
    public async Task Validate_WhenProductNameAndBrandAlreadyExist_ReturnsValidationError() 
    {
        //Arrange
        _mockProductRepository.Setup(repo => repo
            .IsProductNameAndBrandUnique(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var validator = new CreateProductCommandValidator(_mockProductRepository.Object);

        var brandId = TestSeedData.nikeGuid;
        var categoryId = TestSeedData.shoesGuid;
        var productCommand = new CreateProductCommand()
        {
            Name = "Pegasus Nike Runners",
            Price = 99.99M,
            BrandId = brandId,
            CategoryId = categoryId,
            Stocks = {
                new() { Size = "L", InStore = 0, Online = 1 }
            }
        };

        //Act
        var result = await validator.ValidateAsync(productCommand, CancellationToken.None);
        
        //Assert
        result.IsValid.Should().BeFalse();

        var expectedErrorMessage = $"A product with the same name and brand already exists.";
        result.Errors.Find(f => f.ErrorMessage == expectedErrorMessage).Should().NotBeNull();
    }
}
