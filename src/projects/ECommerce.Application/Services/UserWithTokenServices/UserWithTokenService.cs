using Core.Security.Entities;
using Core.Security.JWT;
using ECommerce.Application.Services.Repositories;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.UserWithTokenServices;

public sealed class UserWithTokenService : IUserWithTokenService
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IUserOperationClaimRepository _operationClaimRepository;

    public UserWithTokenService(ITokenHelper tokenHelper, IUserOperationClaimRepository operationClaimRepository)
    {
        _tokenHelper = tokenHelper;
        _operationClaimRepository = operationClaimRepository;
    }
    public async Task<AccessToken> CreateAccessTokenAsync(AppUser user)
    {
        List<OperationClaim> claims =
            await _operationClaimRepository
                .Query()
                .AsNoTracking()
                .Where(p => p.UserId == user.Id)
                .Select(o => new OperationClaim { Id = o.OperationClaimId, Name = o.OperationClaim.Name })
                .ToListAsync();

        AccessToken token = _tokenHelper.CreateToken(user, claims);
        return token;
    }
}