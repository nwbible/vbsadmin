using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VBSAdmin.Data;
using VBSAdmin.Models;
using VBSAdmin.Data.VBSAdminModels;
using VBSAdmin.Models.TenantViewModels;


namespace VBSAdmin
{
    [Authorize(Policy = "SystemAdminOnly")]
    public class TenantsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public TenantsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tenants.ToListAsync());
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.SingleOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChurchName,ContactEmail,ContactName,ContactPhone,Name,Password,ConfirmPassword")] CreateViewModel createModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: Replace with automapper
                Tenant tenant = new Data.VBSAdminModels.Tenant();
                tenant.ChurchName = createModel.ChurchName;
                tenant.ContactEmail = createModel.ContactEmail;
                tenant.ContactName = createModel.ContactName;
                tenant.ContactPhone = createModel.ContactPhone;
                tenant.Name = createModel.Name;


                //Add the new tenant to the database
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                var tenantId = _context.Tenants.First().Id;

                //Add the tenant admin user to the system
                var user = new ApplicationUser { UserName = createModel.ContactEmail, Email = createModel.ContactEmail };
                var result = await _userManager.CreateAsync(user, createModel.Password);
                if (result.Succeeded)
                {
                    var claimResult = await _userManager.AddClaimAsync(user, new Claim(Constants.TenantClaim, tenantId.ToString()));
                    claimResult = await _userManager.AddClaimAsync(user, new Claim(Constants.TenantAdminClaim, "True"));

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                }
                AddErrors(result);

                return RedirectToAction("Index");
            }

            return View(createModel);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.SingleOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChurchName,ContactEmail,ContactName,ContactPhone,Name")] Tenant tenant)
        {
            if (id != tenant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var origTenant = _context.Tenants.AsNoTracking().First<Tenant>(t => t.Id == id);
                    var currentEmail = origTenant.ContactEmail;
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();

                    //Update the user username and email if the email address changed.
                    if (currentEmail != tenant.ContactEmail)
                    {
                        var user = await _userManager.FindByEmailAsync(currentEmail);
                        var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(user, tenant.ContactEmail);
                        var result = await _userManager.ChangeEmailAsync(user, tenant.ContactEmail, changeEmailToken);
                        result = await _userManager.SetUserNameAsync(user, tenant.ContactEmail);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.SingleOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();

            //Need to remove all users associated with this tenant
            //For now we'll just delete the tenant admin user
            var user = await _userManager.FindByEmailAsync(tenant.ContactEmail);
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

        private bool TenantExists(int id)
        {
            return _context.Tenants.Any(e => e.Id == id);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}
