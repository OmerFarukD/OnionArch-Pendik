using ECommerce.Application.Services.UserServices;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services.RoleServices;

public interface IRoleService
{
    Task AddRoleToUserAsync(AppUser user, string roleName);

}