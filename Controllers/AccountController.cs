using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyTaskTracker.Data;
using MyTaskTracker.Helpers;
using MyTaskTracker.Models;
using MyTaskTracker.ViewModels;

namespace MyTaskTracker.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TaskTrackerDbContext _context;

    public AccountController(TaskTrackerDbContext context, UserManager<User> userManager,
            SignInManager<User> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // GET
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

        if (user != null)
        {
            //User is found, check password
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (passwordCheck)
            {
                //Password correct, sign in
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            //Password is incorrect
            TempData["Error"] = "Неверные данный для входа, попробуйте еще раз";
            return View(loginViewModel);
        }
        //User not found
        TempData["Error"] = "Пользователь не найден, попробуйте еще раз";
        return View(loginViewModel);
    }

    [HttpGet]
    public IActionResult Registration()
    {
        var response = new RegistrationViewModel();
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
        if (user != null)
        {
            TempData["Error"] = "Данный Email уже используется";
            return View(registerViewModel);
        }

        var newUser = new User()
        {
            Email = registerViewModel.Email,
            UserName = registerViewModel.Email,
            Name = registerViewModel.Name,
            Surname = registerViewModel.Surname,
            EmailConfirmed = true
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        if (newUserResponse.Succeeded)
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

        return RedirectToAction("Privacy", "Home");
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }

    [HttpGet]
    [Route("Account/Welcome")]
    public async Task<IActionResult> Welcome(int page = 0)
    {
        if(page == 0)
        {
            return View();
        }
        return View();
            
    }
    
}