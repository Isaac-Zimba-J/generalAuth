using Microsoft.AspNetCore.Identity;

namespace Shared.Services;

public interface IUserService 
{
    //register
    Task<IdentityResult> RegisterAsync(ApplicationUser email, string password);
    //login
    Task<SignInResult> LoginAsync(string email, string password);
    //update user
    Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
    //delete user
    Task<IdentityResult> DeleteUserAsync(string userId);
    
}