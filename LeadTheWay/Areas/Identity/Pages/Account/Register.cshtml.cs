using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LeadTheWay.Data;
using LeadTheWay.Models;
using LeadTheWay.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LeadTheWay.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;

            _roleManager = roleManager;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            public string GraphString { get; set; }

            [Display(Name = "Admin")]
            public bool IsAdmin { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Name = Input.Name, PhoneNumber = Input.PhoneNumber, GraphString = "" };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.EndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.EndUser));
                    }

                    if (!await _roleManager.RoleExistsAsync(StaticDetails.AdminUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.AdminUser));
                    }

                    if (Input.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.AdminUser);

                        //if ((await _db.GraphMaps.FirstOrDefaultAsync()).GraphString == null)
                        //{
                        //    (await _db.GraphMaps.FirstOrDefaultAsync()).GraphString = "";
                        //    user.GraphString = "";
                        //}
                        //else
                        //{
                        //    user.GraphString = (await _db.GraphMaps.FirstOrDefaultAsync()).GraphString;
                        //}
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.EndUser);
                    }

                    //(await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == user.Id)).GraphString = user.GraphString;

                    //await _db.SaveChangesAsync();


                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
