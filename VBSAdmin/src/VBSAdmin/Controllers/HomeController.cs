using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VBSAdmin.Models;

namespace VBSAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger _logger;
        private readonly IAuthorizationService authorizationService;


        public HomeController(
           UserManager<ApplicationUser> userMgr,
           SignInManager<ApplicationUser> signInMgr,
           ILoggerFactory loggerFactory,
           IAuthorizationService authorizationSvc)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            authorizationService = authorizationSvc;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            if(signInManager.IsSignedIn(User))
            {
                var sysAdminClaim = User.Claims.FirstOrDefault(c => c.Type == "SystemAdmin");
                var tenantAdminClaim = User.Claims.FirstOrDefault(c => c.Type == "TenantAdmin");

                if(sysAdminClaim != null && sysAdminClaim.Value.ToLower() == "true")
                {
                    return RedirectToAction("Index", "Tenants");
                }
                else
                {
//                    {
//                        return RedirectToAction("Index", "VBS");
//                    }
//                    else
//                    {
                        var currentVbsId = Convert.ToInt32(Request.Cookies[Constants.CurrentVBSIdCookie]);
                        return RedirectToAction("Details", "VBS", new { Id = currentVbsId });
//                    }
                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
