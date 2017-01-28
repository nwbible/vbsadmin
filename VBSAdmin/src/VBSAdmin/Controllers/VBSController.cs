using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VBSAdmin.Data;
using VBSAdmin.Data.VBSAdminModels;
using VBSAdmin.Models.VBSViewModels;
using VBSAdmin.Authorization;

namespace VBSAdmin.Controllers
{

    [Authorize(Policy = "BelongToTenant")]
    public class VBSController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public VBSController(ApplicationDbContext context) : base(context)
        {
            _context = context;    
        }

        // GET: VBS
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VBS.Include(v => v.Tenant).Where(t => t.Tenant.Id == this.TenantId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VBS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Need to verify the requested VBS belongs to the tenant the user belongs to
            var vBS = await _context.VBS.Include(v => v.Tenant).Where(vb => vb.Id == id && vb.TenantId == this.TenantId).SingleOrDefaultAsync();
            if (vBS == null)
            {   
                return NotFound();
            }

            return View(vBS);
        }

        // GET: VBS/Current/5
        public async Task<IActionResult> Current(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Need to verify the requested VBS belongs to the tenant the user belongs to
            var vBS = await _context.VBS.Include(v => v.Tenant).Where(vb => vb.Id == id && vb.TenantId == this.TenantId).SingleOrDefaultAsync();
            if (vBS == null)
            {
                return NotFound();
            }

            return View(vBS);
        }


        // POST: VBS/Current/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Current(int id, [Bind("Id,EndDate,StartDate,ThemeName")] VBS vBS)
        {
            if (id != vBS.Id)
            {
                return NotFound();
            }

            //Need to verify the requested VBS belongs to the tenant the user belongs to
            var vBSCheck = await _context.VBS.Include(v => v.Tenant).Where(vb => vb.Id == id && vb.TenantId == this.TenantId).SingleOrDefaultAsync();
            if (vBSCheck == null)
            {
                return NotFound();
            }

            //Set the cookie to make the specified VBS the default context
            Response.Cookies.Append(Constants.CurrentVBSIdCookie, id.ToString());

            return RedirectToAction("Index");
        }



        // GET: VBS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VBS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EndDate,StartDate,ThemeName,AMStartTime,AMEndTime,AMMaxChildren,PMStartTime,PMEndTime,PMMaxChildren")] CreateViewModel vBSCreateViewModel)
        {


            var vBS = new VBS {
                ThemeName = vBSCreateViewModel.ThemeName,
                EndDate = vBSCreateViewModel.EndDate,
                StartDate = vBSCreateViewModel.StartDate,
                TenantId = this.TenantId
            };

            var amSession = new Session
            {
                Period = Enums.SessionPeriod.AM,
                StartTime = vBSCreateViewModel.AMStartTime,
                EndTime = vBSCreateViewModel.AMEndTime,
                MaxChildren = vBSCreateViewModel.AMMaxChildren
            };

            var pmSession = new Session
            {
                Period = Enums.SessionPeriod.PM,
                StartTime = vBSCreateViewModel.PMStartTime,
                EndTime = vBSCreateViewModel.PMEndTime,
                MaxChildren = vBSCreateViewModel.PMMaxChildren
            };

            vBS.Sessions = new List<Session>();
            vBS.Sessions.Add(amSession);
            vBS.Sessions.Add(pmSession);


            if (ModelState.IsValid)
            {
                _context.Add(vBS);
                _context.Add(amSession);
                _context.Add(pmSession);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "ChurchName", vBS.TenantId);
            return View(vBSCreateViewModel);
        }

        // GET: VBS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Need to verify the requested VBS belongs to the tenant the user belongs to
            var vBS = await _context.VBS.Include(v => v.Tenant).Where(vb => vb.Id == id && vb.TenantId == this.TenantId).SingleOrDefaultAsync();
            //var vBS = await _context.VBS.SingleOrDefaultAsync(m => m.Id == id);
            if (vBS == null)
            {
                return NotFound();
            }
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "ChurchName", vBS.TenantId);
            return View(vBS);
        }

        // POST: VBS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EndDate,StartDate,ThemeName")] VBS vBS)
        {
            if (id != vBS.Id)
            {
                return NotFound();
            }

            //Set the tenant id based on the current user context.
            vBS.TenantId = this.TenantId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vBS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VBSExists(vBS.Id))
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
            return View(vBS);
        }

        // GET: VBS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Need to verify the requested VBS belongs to the tenant the user belongs to
            var vBS = await _context.VBS.Include(v => v.Tenant).Where(vb => vb.Id == id && vb.TenantId == this.TenantId).SingleOrDefaultAsync();
            //var vBS = await _context.VBS.SingleOrDefaultAsync(m => m.Id == id);
            if (vBS == null)
            {
                return NotFound();
            }

            return View(vBS);
        }

        // POST: VBS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var vBS = await _context.VBS.Include(v => v.Tenant).Where(vb => vb.Id == id && vb.TenantId == this.TenantId).SingleOrDefaultAsync();
            //var vBS = await _context.VBS.SingleOrDefaultAsync(m => m.Id == id);
            _context.VBS.Remove(vBS);
            await _context.SaveChangesAsync();

            //TODO: Need to reset the cookie if the current VBS was deleted.

            return RedirectToAction("Index");
        }

        private bool VBSExists(int id)
        {
            return _context.VBS.Any(e => e.Id == id);
        }
    }
}
