namespace ECommerce.Application.Features.Auth.Rules;

public class UserAddRuleMessage
{
    public string UserNameMinCharacterMessage { get; set; }
    public string PasswordMinCharacterMessage { get; set; }
}