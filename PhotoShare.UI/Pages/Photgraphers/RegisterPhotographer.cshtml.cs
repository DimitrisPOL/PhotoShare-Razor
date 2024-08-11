using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PhotoShare.Data;
using PhotoShare.Infrastructure.Data.Users;


namespace PhotoShare.Areas.Identity.Pages.Photgraphers
{
    [Authorize(Roles = "ADMIN")]
    public class RegisterPhotographersModel : PageModel
    {
        private readonly SignInManager<PhotographyUser> _signInManager;
        private readonly UserManager<PhotographyUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterPhotographersModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;


        public RegisterPhotographersModel(
            UserManager<PhotographyUser> userManager,
            SignInManager<PhotographyUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterPhotographersModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
         }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        public string ReturnUrl { get; set; }

        public string UserRoleToRegister { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full name")]
            public string Name { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[] UserPhoto { get; set; }

            [DataType(DataType.Text)]
            [HiddenInput]
            public string UserRoleToRegister { get; set; }

            [Required]
            [Display(Name = "Birth Date")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }


            [Display(Name = "City")]
            public string City { get; set; }


            [Display(Name = "Years Of Expirience")]
            public int YearsOfExpirience { get; set; }

            [Display(Name = "Degree/Certificate")]
            public string Degree { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string userRoleToRegister, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ViewData["UserRoleToRegister"] = userRoleToRegister;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile upload, string returnUrl = null)
        {
            //  Input.UserPhoto = upload;

            bool x = _roleManager.RoleExistsAsync("ADMIN").Result;
            if (!x)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "ADMIN";
                await _roleManager.CreateAsync(role);
            }
            x = _roleManager.RoleExistsAsync("PHOTOGRAPHER").Result;
            if (!x)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "PHOTOGRAPHER";
                await _roleManager.CreateAsync(role);
            }


            if (upload != null && upload.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    upload.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    Input.UserPhoto = ms.ToArray();
                    // string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new PhotographyUser
                {
                    Name = Input.Name,
                    DOB = Input.DOB,
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    ProfilePic = Input.UserPhoto,
                    City = Input.City,
                    Degree = Input.Degree,
                    YearsOfExperience = Input.YearsOfExpirience,
                    DisplayToFront = false
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    IdentityResult identityResult = default;

                     identityResult = await _userManager.AddToRoleAsync(user, "PHOTOGRAPHER");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var s0 = RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        return s0;
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToPage("/PhotographersPage/Index");
        }
    }
}
