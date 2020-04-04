using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELECCIONES.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ELECCIONES.LDTO;
using Microsoft.AspNetCore.Authorization;




namespace ELECCIONES.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        

        public AccountController(UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Register()
        {
            return View();
        }


        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginLDTO lDTO)
        {
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(lDTO.UserName, lDTO.Password, lDTO.RememberME, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Admin", "Home");
                }
                

                
                ModelState.AddModelError(string.Empty, "Tu ta feo wawawaaaaaa.........");
                

                
            }

            return View(lDTO);
        }


        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register(RegisterLDTO lDTO,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = lDTO.UserName, Email = lDTO.Email };
                var result = await userManager.CreateAsync(user, lDTO.Password);

                if (result.Succeeded)
                {
                    if (! string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            

            return View(lDTO);

        }


    }


}