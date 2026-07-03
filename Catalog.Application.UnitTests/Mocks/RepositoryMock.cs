using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using Moq;
using System.Reflection.Emit;

namespace Catalog.Application.UnitTests.Mocks;

public class RepositoryMocks
{
    public static Mock<IProductRepository> GetProductRepository()
    {
        var products = TestSeedData.GetProducts();

        var mockProductRepository = new Mock<IProductRepository>();

        mockProductRepository.Setup(repo => repo.ListAllAsAsync()).ReturnsAsync(products);

        mockProductRepository.Setup(repo => repo
            .AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(
                (Product product) =>
                    {
                        products.Add(product);
                        return product;
                    });

        mockProductRepository.Setup(repo => repo
            .GetProductDetailAsync(It.IsAny<Guid>()))
            .ReturnsAsync(  
                (Guid id) => 
                    products.FirstOrDefault(p => p.Id == id));


        return mockProductRepository;
    }
}
