using System.Net.Http.Json;
using Shared;

public class AuthService(HttpClient httpClient)
{

    public async Task<HttpResponseMessage> RegisterAsync(RegisterModel model)
    {
        return await httpClient.PostAsJsonAsync("register", model);
    }

    public async Task<HttpResponseMessage> LoginAsync(string email, string password)
    {
        
        return await httpClient.PostAsJsonAsync("login", new { email, password });
        // return await httpClient.GetFromJsonAsync<HttpResponseMessage>("login");
    }

    public async Task<HttpResponseMessage> AssignRoleAsync(AssignRoleModel model)
    {
        return await httpClient.PostAsJsonAsync("api/Role/AssignRole", model);
    }

    public async Task<HttpResponseMessage> CreateRoleAsync(string role)
    {
        return await httpClient.PostAsJsonAsync("api/Role/CreateRole", role);
    }
    
    // 
}