using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerOne.Data;

namespace ServerOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BaseDbContext context;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, BaseDbContext _context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            context = _context;
        }

        [HttpPost("CreateRole")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                return Conflict("Role already exists.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return Ok("Role created successfully.");
            }

            return BadRequest("Failed to create role.");
        }

        [HttpPost("AssignRole")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                return NotFound("Role not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok("Role assigned successfully.");
            }

            return BadRequest("Failed to assign role.");
        }
        
        // add delete and update role methods
        [HttpDelete("DeleteRole")]
        // [Authorize]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok("Role deleted successfully.");
            }

            return BadRequest("Failed to delete role.");
        }
        
        //get all roles
        [HttpGet("GetRoles")]
        // [Authorize]
        public IActionResult GetRoles() 
        {
            var roles = context.UserRoles.FromSqlRaw("CALL GetRoles()").ToList();
            return Ok(roles);
        }
    }
}