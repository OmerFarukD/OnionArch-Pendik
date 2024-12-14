using Core.Security.Dtos;
using Core.Security.JWT;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Services.UserServices;
using ECommerce.Application.Services.UserWithTokenServices;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Auth.Commands.AuthLogin;

public static class Login
{
    public sealed record Command(UserForLoginDto LoginDto) : IRequest<AccessToken>;
    
    
    public sealed class Handler : IRequestHandler<Command,AccessToken>
    {
        private readonly IUserService _userService;
        private readonly IUserWithTokenService _userWithTokenService;
        private readonly UserBusinessRules _userBusinessRules;

        public Handler(IUserService userService, IUserWithTokenService userWithTokenService, UserBusinessRules userBusinessRules)
        {
            _userService = userService;
            _userWithTokenService = userWithTokenService;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<AccessToken> Handle(Command request, CancellationToken cancellationToken)
        {
            // email kontrolünü yap
            AppUser? user = await _userService.GetAsync(
                predicate: x => x.Email == request.LoginDto.Email,
                cancellationToken: cancellationToken
            );

            await _userBusinessRules.UserIsPresent(user);
            await _userBusinessRules.UserPasswordShouldBeMatch(user.Id,request.LoginDto.Password);
            // Kullanıcı var mı yok mu kontrolü

            AccessToken accessToken = await _userWithTokenService.CreateAccessTokenAsync(user);

            return accessToken;
        }
    }
}