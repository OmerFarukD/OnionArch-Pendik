using System.Linq.Expressions;
using Core.Persistence.Extensions;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Services.Repositories;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services.UserServices;

// RegisterCommandHandler(IUserService)

public sealed class UserService(IAppUserRepository _userRepository, UserBusinessRules _userBusiness) : IUserService
{
    public async Task<AppUser?> GetAsync(Expression<Func<AppUser, bool>> predicate, bool include = true, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return user;
    }

    public async Task<Paginate<AppUser>> GetPaginateAsync(Expression<Func<AppUser, bool>>? predicate = null, Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>? orderBy = null, bool include = true, int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        var userPaginate = await _userRepository.GetPaginateAsync(predicate, orderBy, include, index, size, withDeleted,
            enableTracking, cancellationToken);

        return userPaginate;
    }

    public async Task<List<AppUser>> GetListAsync(Expression<Func<AppUser, bool>>? predicate = null, Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>? orderBy = null, bool include = true, bool withDeleted = false,
        bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetListAsync(predicate, orderBy, include, withDeleted, enableTracking,
            cancellationToken);

        return users;
    }

    public async Task<AppUser?> AddAsync(AppUser user)
    {
        await _userBusiness.UserEmailShouldNotExistsWhenInserted(user.Email);

        var created = await _userRepository.AddAsync(user);
        return created;

    }

    public async Task<AppUser?> UpdateAsync(AppUser user)
    {
        await _userBusiness.UserEmailShouldNotExistsWhenUpdated(user.Id, user.Email);

        var updated = await _userRepository.UpdateAsync(user);
        return updated;
    }

    
    // 1 omer.dogan@gmail.com
    // 2
    public async Task<AppUser?> DeleteAsync(AppUser user, bool permanent = false)
    {
        var deleted = await _userRepository.DeleteAsync(user, permanent);
        return deleted;
    }
}