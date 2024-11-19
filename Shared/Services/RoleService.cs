using System.Net.Http.Json;

namespace Shared.Services;

public class RoleService(HttpClient httpClient)
{
    public async Task<HttpResponseMessage> CreateRoleAsync(string role)
    {
        return await httpClient.PostAsJsonAsync("api/Role/CreateRole", role);
    }

}