using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Services;
using Shared.Services.Contracts;

namespace ServerOne.Services;

public class UserService(UserManager<ApplicationUser> userManager) :IUserService
{
    public async Task<IdentityResult> RegisterAsync(ApplicationUser user)
    {
        // throw new NotImplementedException();
        
        var result = await userManager.CreateAsync(user, user.PasswordHash);
        return result;
    }

    public async Task<SignInResult> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
    {
        // throw new NotImplementedException();

        var newUser = new ApplicationUser()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber
        };
        
        //fetch user 
        var user1 = await userManager.FindByIdAsync(user.Id);
        
        //update user
        user1.Email = newUser.Email;
        user1.UserName = newUser.UserName;
        user1.PhoneNumber = newUser.PhoneNumber;
        
        //save changes
        var result = await userManager.UpdateAsync(user1);
        return result;
        
    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        // throw new NotImplementedException();
        var user1 = await userManager.FindByIdAsync(userId);
        var result = await userManager.DeleteAsync(user1);
        return result;
    }
    
    // get all users 
    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        // throw new NotImplementedException();
        var users = await userManager.Users.ToListAsync();
        return users;
    }
}