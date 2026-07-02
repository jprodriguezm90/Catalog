using Catalog.Application.Contracts.Persistence;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0);

        RuleFor(p => p.BrandId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleForEach(p => p.Stocks)
            .NotNull().WithMessage("Stock cannot be null.")
            .ChildRules(stocks =>
            {
                stocks.RuleFor(s => s.Size)
                    .NotEmpty().WithMessage("{PropertyName} is required.");

                stocks.RuleFor(s => s.InStore)
                    .GreaterThanOrEqualTo(0);

                stocks.RuleFor(s => s.Online)
                    .GreaterThanOrEqualTo(0);

            });

        RuleFor(e => e)
            .MustAsync(ProductNameAndBrandUnique)
            .WithMessage("A product with the same name and brand already exists.");
    }

    private async Task<bool> ProductNameAndBrandUnique(CreateProductCommand e, CancellationToken token)
    {
        return await _productRepository.IsProductNameAndBrandUnique(e.Name, e.BrandId);
    }
}
