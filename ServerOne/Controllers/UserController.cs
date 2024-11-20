using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Services;

namespace ServerOne.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : Controller
{

    [HttpPost]
    public async Task<IActionResult> UpdateUserAsync(ApplicationUser user)
    {
        var result = await service.UpdateUserAsync(user);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync(string userId)
    {
        var result = await service.DeleteUserAsync(userId);
        return Ok(result);
    }
    
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var result = await service.GetAllUsersAsync();
        return Ok(result);
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(ApplicationUser user)
    {
        var result = await service.RegisterAsync(user);
        return Ok(result);
    }
}