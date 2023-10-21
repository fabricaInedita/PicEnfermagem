using Microsoft.AspNetCore.Identity;
using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Identity.Configuration;

public class EmailConfirmationTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser>
    where TUser : ApplicationUser
{

    public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        if (manager != null && user != null)
        {
            return Task.FromResult(true);
        }
        else
        {
            return Task.FromResult(false);
        }
    }
    private string GenerateToken(ApplicationUser user, string purpose)
    {
        string secretString = "coffeIsGood";
        return secretString + user.Email + purpose + user.Id;
    }

    public Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
    {
        return Task.FromResult(GenerateToken(user, purpose));
    }

    public Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    {
        return Task.FromResult(token == GenerateToken(user, purpose));
    }
}

public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public EmailConfirmationTokenProviderOptions()
    {
        Name = "Default";
        TokenLifespan = TimeSpan.FromDays(1);
    }
}
