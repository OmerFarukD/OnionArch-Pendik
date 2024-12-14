using Core.Security.Entities;
using ECommerce.Application.Services.Repositories;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.RoleServices;

public sealed class RoleService(
    IUserOperationClaimRepository _userOperationClaimRepository,
    IOperationClaimRepository _operationClaimRepository
    
) : IRoleService
{
    public async Task AddRoleToUserAsync(AppUser user, string roleName)
    {
        var roleId = _operationClaimRepository
            .Query()
            .AsTracking()
            .Where(x => x.Name == roleName)
            .Select(x => x.Id)
            .SingleOrDefault();


        await _userOperationClaimRepository
            .AddAsync(new UserOperationClaim(user.Id,roleId));
        
    }
}