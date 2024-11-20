using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;

namespace Shared.Services.Contracts;

public class UserService(HttpClient httpClient) : IUserService
{
    public async Task<IdentityResult> RegisterAsync(ApplicationUser  user)
    {
        // throw new NotImplementedException();
        var response = await httpClient.PostAsJsonAsync("api/User/Register", user);
        return await response.Content.ReadFromJsonAsync<IdentityResult>();
    }

    public async Task<SignInResult> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
    {
        // throw new NotImplementedException()
        var response =  await httpClient.PostAsJsonAsync("api/User/UpdateUser", user);
        return await response.Content.ReadFromJsonAsync<IdentityResult>();
        

    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        // throw new NotImplementedException();
        var response = await httpClient.DeleteAsync($"api/User/DeleteUser/{userId}");
        return await response.Content.ReadFromJsonAsync<IdentityResult>();
    }
    
    // get all users
    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        // throw new NotImplementedException();
        return await httpClient.GetFromJsonAsync<List<ApplicationUser>>("api/User/GetAllUsers");
    }
}