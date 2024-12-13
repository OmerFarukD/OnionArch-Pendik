namespace ECommerce.Application.Features.Auth.Rules;

public class UserAddRule
{
    public int UserNameMinCharacter { get; set; }
    public int PasswordMinCharacter { get; set; }
}