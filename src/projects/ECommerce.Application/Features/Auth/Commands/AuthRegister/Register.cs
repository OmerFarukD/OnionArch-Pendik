using Core.Security.Constants;
using Core.Security.Dtos;
using Core.Security.Hashing;
using Core.Security.JWT;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Services.Repositories;
using ECommerce.Application.Services.RoleServices;
using ECommerce.Application.Services.UserServices;
using ECommerce.Application.Services.UserWithTokenServices;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Auth.Commands.AuthRegister;

public static class Register
{
    public sealed record Command(UserForRegisterDto UserForRegisterDto) : IRequest<AccessToken>;
    
    
    public sealed class Handler : IRequestHandler<Command,AccessToken>
    {
        private readonly IUserService _userService;
        private readonly IUserWithTokenService _userWithTokenService;
        private readonly IRoleService _roleService;

        public Handler(IUserService userService, IUserWithTokenService userWithTokenService, IRoleService roleService)
        {
            _userService = userService;
            _userWithTokenService = userWithTokenService;
            _roleService = roleService;
        }


        public async Task<AccessToken> Handle(Command request, CancellationToken cancellationToken)
        {
            var register = request.UserForRegisterDto;
            
            HashingHelper.CreatePasswordHash(register.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt
                );

            AppUser user = new()
            {
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,

            };

            AppUser? created = await _userService.AddAsync(user);

            await _roleService.AddRoleToUserAsync(created, GeneralOperationClaims.User);
           
            

            AccessToken accessToken =await _userWithTokenService.CreateAccessTokenAsync(created);

            return accessToken;

        }
    }
}