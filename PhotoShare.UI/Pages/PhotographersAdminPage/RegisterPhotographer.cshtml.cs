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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PhotoShare.Data;
using PhotoShare.Domain.Values;
using PhotoShare.Infrastructure.Data.Users;


namespace PhotoShare.Pages.PhotographersAdminPage
{
    [Authorize(Roles = "admin")]
    public class RegisterPhotographersModel : PageModel
    {
        private readonly SignInManager<PhotographyUser> _signInManager;
        private readonly UserManager<PhotographyUser> _userManager;
        private readonly ILogger<RegisterPhotographersModel> _logger;
        private readonly IEmailSender _emailSender;
        public List<SelectListItem> Options { get; set; }


        public RegisterPhotographersModel(
            UserManager<PhotographyUser> userManager,
            SignInManager<PhotographyUser> signInManager,
            ILogger<RegisterPhotographersModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            Options = new List<SelectListItem> { new SelectListItem("Admin", "ADMIN"), new SelectListItem("Photographer", "PHOTOGRAPHER") };
        }

        [BindProperty]
        public Photgraphers.Models.InputModel Input { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }



        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile upload, string returnUrl = null)
        {

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
                        values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    IdentityResult identityResult = default;

                    identityResult = await _userManager.AddToRoleAsync(user, Input.UserRoleToRegister);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var s0 = RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
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
