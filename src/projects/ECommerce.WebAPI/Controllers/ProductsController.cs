using ECommerce.Application.Features.Products.Commands.Create;
using ECommerce.Application.Features.Products.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : BaseController
{

    [HttpPost("add")]
    public async Task<IActionResult> Add(ProductAddCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetListProductQuery());

        return Ok(response);
    }
    
}