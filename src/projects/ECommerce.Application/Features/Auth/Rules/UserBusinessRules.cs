using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.Security.Entities;
using Core.Security.Hashing;
using ECommerce.Application.Features.Auth.Constants;
using ECommerce.Application.Services.Repositories;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Application.Features.Auth.Rules;

public sealed class UserBusinessRules
{
    private readonly IAppUserRepository _userRepository;
    private IConfiguration Configuration { get;}
    private readonly UserAddRule _userAddRule;
    private readonly UserAddRuleMessage _userAddRuleMessage;

    public UserBusinessRules(IAppUserRepository userRepository,IConfiguration configuration)
    {
        Configuration = configuration;
        const string sectionOfRules = "UserAddRule";
        const string sectionOfRuleMessages = "UserAddRuleMessage";


        _userAddRule = Configuration.GetSection(sectionOfRules).Get<UserAddRule>();
        _userAddRuleMessage = Configuration.GetSection(sectionOfRuleMessages).Get<UserAddRuleMessage>();
        
        
        _userRepository = userRepository;
    }
    public async Task UserEmailShouldNotExistsWhenInserted(string email)
    {
        bool userExists = await _userRepository.AnyAsync(x=>x.Email==email);
        if (userExists)
        {
            throw new AuthorizationException(AuthMessages.UserMailAlreadyExists);
        }
    }

    public async Task UserCheckByAddedRules(string username, string password)
    {
        if (username.Length<_userAddRule.UserNameMinCharacter)
        {
            throw new BusinessException(_userAddRuleMessage.UserNameMinCharacterMessage);
        }
        
        if (password.Length < _userAddRule.PasswordMinCharacter)
        {
            throw new BusinessException(_userAddRuleMessage.PasswordMinCharacterMessage);
        }
    }

    public async Task UserEmailShouldNotExistsWhenUpdated(int id, string email)
    {
        bool userExists = await _userRepository.AnyAsync(x => x.Id != id && email == x.Email);
        if (userExists)
        {
            throw new AuthorizationException(AuthMessages.UserMailAlreadyExists);
        }
    }

    public  Task UserIsPresent(AppUser? user)
    {
        if (user is null)
        {
            throw new AuthorizationException(AuthMessages.UserNotFound);
        }

        return Task.CompletedTask;
    }


    public async Task UserPasswordShouldBeMatch(int id,string password)
    {
        AppUser? user = await _userRepository.GetAsync(predicate: x => x.Id == id);
        await UserIsPresent(user);
        if (!HashingHelper.VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
        {
            throw new AuthorizationException(AuthMessages.WrongPassword);
        }
        
    }
}