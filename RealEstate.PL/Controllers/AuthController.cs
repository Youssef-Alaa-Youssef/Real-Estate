using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using RealEstate.PL.Helper;
using RealEstate.PL.Services.Email;
using RealEstate.PL.ViewModels.Auth;
namespace RealEstate.Controllers
{
    public class AuthController : Controller
    {
        private readonly EmailConfiguration _emailconfig;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CompanyDetails _companydetails;

        public AuthController(EmailConfiguration emailconfig, IWebHostEnvironment environment, IEmailService EmailService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<CompanyDetails> companydetails)
        {
            _emailconfig = emailconfig;
            _environment = environment;
            _emailService = EmailService;
            _userManager = userManager;
            _signInManager = signInManager;
            _companydetails = companydetails.Value;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string query)
        {
            List<IdentityUser> users;

            if (!string.IsNullOrEmpty(query))
            {
                users = await _userManager.Users
                    .Where(u => u.UserName.Contains(query))
                    .ToListAsync();
            }
            else
            {
                users = await _userManager.Users.ToListAsync();
            }

            var userViewModels = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName
            }).ToList();

            ViewBag.Query = query;
            return View(userViewModels);
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid Email Or Password.";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                TempData["Success"] = "Congratulations Login Successfully";
                return RedirectToAction("DashBoard", "Home");
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Your account needs to be activated.");
                ModelState.AddModelError(string.Empty, "Please check your email for the activation link.");
                return View(model);
            }
            TempData["Error"] = "Invalid Email Or Password.";
            return View(model);
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                TempData["Error"] = "Email is already in use.";
                return View(model);
            }

            var newUser = new IdentityUser { PhoneNumber = model.PhoneNumber, UserName = model.UserName, Email = model.Email }; // Adjust this line based on your ApplicationUser model
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = Url.Action("ConfirmEmail", "Auth",
                new { userId = newUser.Id, token = token }, Request.Scheme);
                string subject = "Activate Your Account";

                string body = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Activate Your Account</title>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap');

        body {{
            margin: 0;
            padding: 0;
            font-family: 'Roboto', Arial, sans-serif;
            background-color: #f8f9fa;
            color: #333;
        }}

        .container {{
            width: 100%;
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            border: 1px solid #e0e0e0;
        }}

        .header {{
            background: url('/assets/images/Login.png');
            background-size: cover;
            height: 200px;
            border-bottom: 1px solid #e0e0e0;
        }}

        .content {{
            padding: 30px;
            text-align: center;
        }}

        h1 {{
            font-size: 28px;
            color: #007bff;
            margin-bottom: 20px;
            font-weight: 700;
        }}

        p {{
            font-size: 16px;
            line-height: 1.5;
            margin: 0 0 20px;
        }}

        .button {{
            display: inline-block;
            padding: 15px 30px;
            background-color: #007bff;
            color: #ffffff;
            text-decoration: none;
            border-radius: 5px;
            font-size: 16px;
            font-weight: 700;
            text-align: center;
            margin: 20px 0;
            border: 1px solid #007bff;
        }}

        .button:hover {{
            background-color: #0056b3;
            border-color: #0056b3;
        }}

        .footer {{
            background-color: #f4f4f4;
            text-align: center;
            padding: 20px;
            font-size: 14px;
            color: #666;
        }}

        .footer a {{
            color: #007bff;
            text-decoration: none;
        }}

        .footer a:hover {{
            text-decoration: underline;
        }}

        /* Responsive Styles */
        @media only screen and (max-width: 600px) {{
            .header {{
                height: 150px; /* Adjust header height for smaller screens */
            }}

            h1 {{
                font-size: 20px; /* Adjust font size for smaller screens */
            }}

            .button {{
                padding: 12px 20px; /* Adjust button padding for smaller screens */
                font-size: 14px; /* Adjust font size for smaller screens */
            }}

            .content {{
                padding: 20px; /* Adjust content padding for smaller screens */
            }}
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header""></div>
        <div class=""content"">
            <h1>Activate Your Account</h1>
            <p>Hi {newUser.UserName},</p>
            <p>Thank you for registering with us. To complete your registration, please click the button below to activate your account.</p>
            <a href=""{confirmationLink}"" class=""button"">Activate Account</a>
            <p>If you did not register for an account, you can safely ignore this email.</p>
        </div>
        <div class=""footer"">
            <p>Best regards,</p>
            <p>{_companydetails.Name} - {_companydetails.PhoneNumber}</p>
            <p><a asp-action=""ContactUs"" asp-controller=""Home"">Contact Us</a></p>
        </div>
    </div>
</body>
</html>
";

                await _emailService.SendEmailAsync(newUser.Email, subject, body);


                return RedirectToAction(nameof(Confirmation));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        public IActionResult Confirmation()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["Error"] = "Please provide valid credentials.";

                return RedirectToAction("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["Success"] = "Account Activated Successfully.";
                return RedirectToAction("Activated");
            }

            return RedirectToAction("Error");
        }
        public IActionResult Activated()
        {
            return View();
        }
        public IActionResult forgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> forgotPassword(RestPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    TempData["Error"] = "Account Not Activated";
                    return RedirectToAction(nameof(forgotPassword));
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ActualRestPassword", "Auth", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
                string subject = "Reset Your Password";
                string body = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                            background-color: #f7f7f7;
                            padding: 20px;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #fff;
                            padding: 30px;
                            border-radius: 5px;
                            box-shadow: 0 0 10px rgba(0,0,0,0.1);
                        }}
                        .btn {{
                            display: inline-block;
                            background-color: #007bff;
                            color: #fff;
                            text-decoration: none;
                            padding: 10px 20px;
                            border-radius: 5px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Hello, {user.UserName}</h2>
                        <p>We received a request to reset your password. Click the button below to reset it:</p>
                        <p><a class='btn' href='{callbackUrl}'>Reset Password</a></p>
                        <p>If you did not request a password reset, you can ignore this email.</p>
                        <p>Best regards,<br>{_emailconfig.DisplayName} Application Team</p>
                    </div>
                </body>
                </html>
            ";

                await _emailService.SendEmailAsync(user.Email, subject, body);
                return RedirectToAction(nameof(forgotPassword));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ActualRestPassword(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                TempData["Error"] = "Please provide valid credentials.";
                return RedirectToAction(nameof(forgotPassword));
            }

            var model = new ActualRestPassswordViewModel { UserId = UserId, Token = Token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualRestPassword(ActualRestPassswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    TempData["Error"] = "Please provide valid credentials.";

                    return RedirectToAction(nameof(forgotPassword));
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Reset Password Done Successfully. ";

                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        TempData["Error"] = $"{error.Description}.";
                    }
                    return View(model);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserDetailsViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Details), new { id = user.Id });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserDetailsViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            var viewModel = new UserDetailsViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(viewModel);
        }

        public IActionResult LogOut()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ActualLogOut()
        {
            TempData["Success"] = "You have been successfully logged out";
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = User.Identity.Name; 
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ApplicationUserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };

            return View(model);
        }
        

        public IActionResult Settings()
        {
            return View();
        }
    }
}
