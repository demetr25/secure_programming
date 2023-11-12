using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Roles = "admin")]
public class UsersController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        
        // var res = _roleManager.CreateAsync(new IdentityRole("admin")).Result;
        // if (res.Succeeded)
        // {
        //     Console.WriteLine("Role created");
        // }
        // else
        // {
        //     Console.WriteLine(res.ToString());
        // }
    }

    public async Task<IActionResult> ShowAllUsers()
    {
        var users = _userManager
            .Users
            .ToList();
        return View(users);
    }
}