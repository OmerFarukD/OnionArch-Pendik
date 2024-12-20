using Core.Application.Requests;
using ECommerce.Application.Features.ProductImages.Commands.Create;
using ECommerce.Application.Features.ProductImages.Queries.GetList;
using ECommerce.Application.Features.ProductImages.Queries.GetListByPaginate;
using ECommerce.Application.Features.ProductImages.Queries.GetListByProductId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductImagesController(IMediator mediator) : ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> Upload(ProductImageAddCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }


    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetListProductImageQuery());
        return Ok(response);
    }

    [HttpGet("paginate")]
    public async Task<IActionResult> GetAllPaginate([FromQuery] PageRequest pageRequest)
    {
        var response = await mediator.Send(new GetListProductImageByPaginateQuery
        {
            PageRequest = pageRequest
        });

        return Ok(response);
    }

    [HttpGet("getallbyproductId")]
    public async Task<IActionResult> GetAllImagesByProductId([FromQuery] Guid id)
    {
        var response = await mediator.Send(new GetListByProductIdQuery
        {
            ProductId = id
        });

        return Ok(response);
    }
}