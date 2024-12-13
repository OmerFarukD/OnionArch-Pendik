using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace ECommerce.Application.Services.Repositories;

public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim, int>
{
    
}