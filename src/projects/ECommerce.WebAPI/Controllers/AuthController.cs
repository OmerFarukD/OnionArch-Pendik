using Core.Security.Dtos;
using ECommerce.Application.Features.Auth.Commands.AuthLogin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{


    [HttpPost("login")]
    public async Task<IActionResult> LoginForUser(UserForLoginDto dto)
    {
        var response = await mediator.Send(new Login.Command(dto));
        return Ok(response);
    }
    
}