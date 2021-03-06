﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VBSAdmin.Data;
using VBSAdmin.Models;
using VBSAdmin.Data.VBSAdminModels;
using VBSAdmin.Models.AccountViewModels;
using VBSAdmin.Services;
using VBSAdmin.Authorization;

namespace VBSAdmin.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            ApplicationDbContext dbContext) : base(dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _context = dbContext;
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");

                    //Add the tenant cookie
                    var signedInUser = _userManager.FindByNameAsync(model.Email).Result;
                    var claims = _userManager.GetClaimsAsync(signedInUser).Result;
                    var tenantId = claims.FirstOrDefault(c => c.Type == Constants.TenantClaim).Value;
                    Response.Cookies.Append(Constants.TenantClaim, tenantId);

                    //Add the current VBS id cookie
                    //The default VBS is the one with the highest Id value (assuming that is the latest VBS for the tenant)
                    var vbsContext = _context.VBS.Include(v => v.Tenant).Where(t => t.Tenant.Id == Convert.ToInt32(tenantId)).OrderByDescending(v => v.Id);
                    var latestVbs = vbsContext.FirstOrDefault();
                    Response.Cookies.Append(Constants.CurrentVBSIdCookie, latestVbs.Id.ToString());


                    return RedirectToLocal(returnUrl);
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                //}
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [Authorize(Policy = "TenantAdmin")]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Policy = "TenantAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var claimResult = await _userManager.AddClaimAsync(user, new Claim(Constants.TenantClaim, this.TenantId.ToString()));
                    if (model.IsTenantAdmin)
                    {
                        claimResult = await _userManager.AddClaimAsync(user, new Claim(Constants.TenantAdminClaim, "True"));
                    }

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("TenantUserList");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            Response.Cookies.Delete(Constants.CurrentVBSIdCookie);
            Response.Cookies.Delete(Constants.TenantClaim);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> TenantUserList()
        {
            List<UserListViewModel> usersVMs = new List<UserListViewModel>();

            Claim tenantClaim = new Claim(Constants.TenantClaim, this.TenantId.ToString());

            var tenantUsers = await _userManager.GetUsersForClaimAsync(tenantClaim);

            foreach(ApplicationUser user in tenantUsers)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);

                if (userClaims.First(c => c.Type == Constants.TenantClaim).Value.ToLower() == this.TenantId.ToString().ToLower())
                {
                    UserListViewModel userVM = new UserListViewModel
                    {
                        Name = user.Name,
                        Email = user.Email,
                        TenantId = this.TenantId,
                        UserId = user.Id
                    };

                    if(userClaims.FirstOrDefault(c => c.Type == Constants.TenantAdminClaim) != null &&
                       userClaims.First(c => c.Type == Constants.TenantAdminClaim).Value.ToLower() == "true")
                    {
                        userVM.IsTenantAdmin = "Yes";
                    }
                    else
                    {
                        userVM.IsTenantAdmin = "No";
                    }


                    usersVMs.Add(userVM);
                }
            }

            return View(usersVMs);
        }

        [HttpGet]
        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> EditTenantUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            //Validate user exists in system
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            //Make sure requested user belongs to tenant via claim check.
            if (userClaims.First(c => c.Type == Constants.TenantClaim).Value.ToLower() != this.TenantId.ToString().ToLower())
            {
                return NotFound();
            }

            EditTenantUserViewModel editUserVM = new EditTenantUserViewModel
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            if (userClaims.FirstOrDefault(c => c.Type == Constants.TenantAdminClaim) != null &&
                userClaims.First(c => c.Type == Constants.TenantAdminClaim).Value.ToLower() == "true")
            {
                editUserVM.IsTenantAdmin = true;
            }
            else
            {
                editUserVM.IsTenantAdmin = false;
            }


            return View(editUserVM);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> EditTenantUser(string id, [Bind("UserId,Name,Email,IsTenantAdmin,Password,ConfirmPassword")] EditTenantUserViewModel editTenantUserVM)
        {
            if (id != editTenantUserVM.UserId)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            //Make sure requested user belongs to tenant via claim check.
            if (userClaims.First(c => c.Type == Constants.TenantClaim).Value.ToLower() != this.TenantId.ToString().ToLower())
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                user.Name = editTenantUserVM.Name;
                user.Email = editTenantUserVM.Email;
                user.UserName = editTenantUserVM.Email;

                IdentityResult idResult = await _userManager.UpdateAsync(user);
                if(idResult != IdentityResult.Success)
                {
                    AddErrors(idResult);
                }

                if (!string.IsNullOrWhiteSpace(editTenantUserVM.Password))
                {
                    bool isSamePassword = await _userManager.CheckPasswordAsync(user, editTenantUserVM.Password);
                    if (!isSamePassword)
                    {
                        idResult = await _userManager.RemovePasswordAsync(user);
                        idResult = await _userManager.AddPasswordAsync(user, editTenantUserVM.Password);
                    }
                }

                Claim claim = new Claim(Constants.TenantAdminClaim, "True");
                if (editTenantUserVM.IsTenantAdmin)
                {
                    idResult = await _userManager.AddClaimAsync(user, claim);
                }
                else
                {
                    idResult = await _userManager.RemoveClaimAsync(user, claim);
                }

                return RedirectToAction("TenantUserList");
            }

            return View(editTenantUserVM);
        }


        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> DeleteTenantUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            //Make sure requested user belongs to tenant via claim check.
            var userClaims = await _userManager.GetClaimsAsync(user);
            if (userClaims.First(c => c.Type == Constants.TenantClaim).Value.ToLower() != this.TenantId.ToString().ToLower())
            {
                return NotFound();
            }

            DeleteTenantUserViewModel deleteUserVM = new DeleteTenantUserViewModel
            {
                Name = user.Name,
                Email = user.Email,
                UserId = user.Id
            };

            return View(deleteUserVM);
        }

        [HttpPost, ActionName("DeleteTenantUser")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> DeleteTenantUserConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            //Make sure requested user belongs to tenant via claim check.
            var userClaims = await _userManager.GetClaimsAsync(user);
            if (userClaims.First(c => c.Type == Constants.TenantClaim).Value.ToLower() != this.TenantId.ToString().ToLower())
            {
                return NotFound();
            }

            IdentityResult idResult = await _userManager.DeleteAsync(user);
            if(idResult != IdentityResult.Success)
            {
                AddErrors(idResult);
            }

            return RedirectToAction("TenantUserList");
        }


        /*        //
                // POST: /Account/ExternalLogin
                [HttpPost]
                [AllowAnonymous]
                [ValidateAntiForgeryToken]
                public IActionResult ExternalLogin(string provider, string returnUrl = null)
                {
                    // Request a redirect to the external login provider.
                    var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
                    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                    return Challenge(properties, provider);
                }

                //
                // GET: /Account/ExternalLoginCallback
                [HttpGet]
                [AllowAnonymous]
                public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
                {
                    if (remoteError != null)
                    {
                        ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                        return View(nameof(Login));
                    }
                    var info = await _signInManager.GetExternalLoginInfoAsync();
                    if (info == null)
                    {
                        return RedirectToAction(nameof(Login));
                    }

                    // Sign in the user with this external login provider if the user already has a login.
                    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
                    }
                    if (result.IsLockedOut)
                    {
                        return View("Lockout");
                    }
                    else
                    {
                        // If the user does not have an account, then ask the user to create an account.
                        ViewData["ReturnUrl"] = returnUrl;
                        ViewData["LoginProvider"] = info.LoginProvider;
                        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
                    }
                }

                //
                // POST: /Account/ExternalLoginConfirmation
                [HttpPost]
                [AllowAnonymous]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
                {
                    if (ModelState.IsValid)
                    {
                        // Get the information about the user from the external login provider
                        var info = await _signInManager.GetExternalLoginInfoAsync();
                        if (info == null)
                        {
                            return View("ExternalLoginFailure");
                        }
                        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            result = await _userManager.AddLoginAsync(user, info);
                            if (result.Succeeded)
                            {
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                                return RedirectToLocal(returnUrl);
                            }
                        }
                        AddErrors(result);
                    }

                    ViewData["ReturnUrl"] = returnUrl;
                    return View(model);
                }

                // GET: /Account/ConfirmEmail
                [HttpGet]
                [AllowAnonymous]
                public async Task<IActionResult> ConfirmEmail(string userId, string code)
                {
                    if (userId == null || code == null)
                    {
                        return View("Error");
                    }
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return View("Error");
                    }
                    var result = await _userManager.ConfirmEmailAsync(user, code);
                    return View(result.Succeeded ? "ConfirmEmail" : "Error");
                }

                //
                // GET: /Account/ForgotPassword
                [HttpGet]
                [AllowAnonymous]
                public IActionResult ForgotPassword()
                {
                    return View();
                }

                //
                // POST: /Account/ForgotPassword
                [HttpPost]
                [AllowAnonymous]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
                {
                    if (ModelState.IsValid)
                    {
                        var user = await _userManager.FindByNameAsync(model.Email);
                        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                        {
                            // Don't reveal that the user does not exist or is not confirmed
                            return View("ForgotPasswordConfirmation");
                        }

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                        // Send an email with this link
                        //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                        //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                        //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                        //return View("ForgotPasswordConfirmation");
                    }

                    // If we got this far, something failed, redisplay form
                    return View(model);
                }

                //
                // GET: /Account/ForgotPasswordConfirmation
                [HttpGet]
                [AllowAnonymous]
                public IActionResult ForgotPasswordConfirmation()
                {
                    return View();
                }

                //
                // GET: /Account/ResetPassword
                [HttpGet]
                [AllowAnonymous]
                public IActionResult ResetPassword(string code = null)
                {
                    return code == null ? View("Error") : View();
                }

                //
                // POST: /Account/ResetPassword
                [HttpPost]
                [AllowAnonymous]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    var user = await _userManager.FindByNameAsync(model.Email);
                    if (user == null)
                    {
                        // Don't reveal that the user does not exist
                        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
                    }
                    var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
                    }
                    AddErrors(result);
                    return View();
                }

                //
                // GET: /Account/ResetPasswordConfirmation
                [HttpGet]
                [AllowAnonymous]
                public IActionResult ResetPasswordConfirmation()
                {
                    return View();
                }

                //
                // GET: /Account/SendCode
                [HttpGet]
                [AllowAnonymous]
                public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
                {
                    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
                    if (user == null)
                    {
                        return View("Error");
                    }
                    var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
                    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
                    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
                }

                //
                // POST: /Account/SendCode
                [HttpPost]
                [AllowAnonymous]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> SendCode(SendCodeViewModel model)
                {
                    if (!ModelState.IsValid)
                    {
                        return View();
                    }

                    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
                    if (user == null)
                    {
                        return View("Error");
                    }

                    // Generate the token and send it
                    var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
                    if (string.IsNullOrWhiteSpace(code))
                    {
                        return View("Error");
                    }

                    var message = "Your security code is: " + code;
                    if (model.SelectedProvider == "Email")
                    {
                        await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
                    }
                    else if (model.SelectedProvider == "Phone")
                    {
                        await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
                    }

                    return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                }

                //
                // GET: /Account/VerifyCode
                [HttpGet]
                [AllowAnonymous]
                public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
                {
                    // Require that the user has already logged in via username/password or external login
                    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
                    if (user == null)
                    {
                        return View("Error");
                    }
                    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
                }

                //
                // POST: /Account/VerifyCode
                [HttpPost]
                [AllowAnonymous]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }

                    // The following code protects for brute force attacks against the two factor codes.
                    // If a user enters incorrect codes for a specified amount of time then the user account
                    // will be locked out for a specified amount of time.
                    var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
                    if (result.Succeeded)
                    {
                        return RedirectToLocal(model.ReturnUrl);
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning(7, "User account locked out.");
                        return View("Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid code.");
                        return View(model);
                    }
                }
        */
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
