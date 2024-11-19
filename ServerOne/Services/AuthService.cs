using Microsoft.AspNetCore.Identity;
using Shared;
using Shared.Services.Contracts;

namespace ServerOne.Services;

public class AuthService (HttpClient httpClient, UserManager<ApplicationUser> userManager) 
{
    


    public async Task<HttpResponseMessage> LoginAsync(LoginModel model)
    {
        return await httpClient.PostAsJsonAsync("api/Account/Login", model);
    }

    public async Task<HttpResponseMessage> AssignRoleAsync(AssignRoleModel model)
    {
        return await httpClient.PostAsJsonAsync("api/Role/AssignRole", model);
    }

    public async Task<IdentityResult> RegisterAsync(ApplicationUser email, string password)
    {
        throw new NotImplementedException();
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
}