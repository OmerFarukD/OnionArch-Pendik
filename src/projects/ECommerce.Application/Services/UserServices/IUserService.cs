using System.Linq.Expressions;
using Core.Persistence.Extensions;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services.UserServices;

public interface IUserService
{
    Task<AppUser?> GetAsync(Expression<Func<AppUser, bool>> predicate, bool include = true, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default);
   
    Task<Paginate<AppUser>> GetPaginateAsync(
        Expression<Func<AppUser, bool>>? predicate = null,
        Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>? orderBy = null,
        bool include = true,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default

    );
    
    
    Task<List<AppUser>> GetListAsync(
        Expression<Func<AppUser, bool>>? predicate = null,
        Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>? orderBy = null,
        bool include = true,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default

    );

    Task<AppUser?> AddAsync(AppUser user);
    Task<AppUser?> UpdateAsync(AppUser user);
    Task<AppUser?> DeleteAsync(AppUser user, bool permanent = false);

}