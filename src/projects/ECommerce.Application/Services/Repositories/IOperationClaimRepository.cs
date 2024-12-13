using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace ECommerce.Application.Services.Repositories;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim, int>
{
    
}