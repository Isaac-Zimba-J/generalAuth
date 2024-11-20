using Microsoft.AspNetCore.Identity;

namespace Shared.Services;

public interface IUserService 
{
    //register
    Task<IdentityResult> RegisterAsync(ApplicationUser user);
    //login
    Task<SignInResult> LoginAsync(string email, string password);
    //update user
    Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
    //delete user
    Task<IdentityResult> DeleteUserAsync(string userId);
    
    // get all users
    Task<List<ApplicationUser>> GetAllUsersAsync();
    
}