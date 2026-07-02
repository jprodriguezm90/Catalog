using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.Features.Products.Queries.GetProductDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost(Name= "AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var getEventDetailQuery = new GetProductDetailQuery() { ProductId = id };
        return Ok(await mediator.Send(getEventDetailQuery));
    }
}
