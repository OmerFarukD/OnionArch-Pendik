using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.Security.Constants;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            throw new AuthorizationException("You are not authenticated.");
        }

        // Kullanıcı rollerini al
        var userRoleClaims = httpContext.User.ClaimRoles();

        if (userRoleClaims == null || !userRoleClaims.Any())
        {
            throw new AuthorizationException("You are not authenticated.");
        }

    
        bool isAuthorized = userRoleClaims.Contains(GeneralOperationClaims.Admin) ||
                            request.Roles.Any(role => userRoleClaims.Contains(role));

        if (!isAuthorized)
        {
            throw new AuthorizationException("Yetkiniz Yok");
        }

      
        return await next();
    }

}