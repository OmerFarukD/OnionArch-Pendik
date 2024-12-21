using ECommerce.Application.Features.Products.Commands.Create;
using ECommerce.Application.Features.Products.Queries.GetList;
using ECommerce.Application.Features.Products.Queries.GetListByElasticSearch;
using ECommerce.Application.Features.Products.Queries.GetListByImages;
using ECommerce.Application.Features.Products.Queries.GetListFilterByElasticSearch;
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

    [HttpGet("getallbyimages")]
    public async Task<IActionResult> GetAllByImageUrls()
    {
        var response = await mediator.Send(new GetListProductByProductImageQuery());
        return Ok(response);
    }

    [HttpGet("elasticall")]
    public async Task<IActionResult> GetAllElastic()
    {
        var response = await mediator.Send(new GetProductListElasticSearchQuery());
        return Ok(response);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetAllByFilter([FromQuery]string text)
    {
        var query = new GetProductListFilterByElasticSearchQuery() { Text = text };

        var response = await mediator.Send(query);
        return Ok(response);
    }
    
}