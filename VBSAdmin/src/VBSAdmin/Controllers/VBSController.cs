using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VBSAdmin.Data;
using VBSAdmin.Models.VBSAdminModels;

namespace VBSAdmin
{

    [Authorize(Policy = "BelongToTenant")]
    public class VBSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VBSController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: VBS
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VBS.Include(v => v.Tenant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VBS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vBS = await _context.VBS.SingleOrDefaultAsync(m => m.Id == id);
            if (vBS == null)
            {
                return NotFound();
            }

            return View(vBS);
        }

        // GET: VBS/Create
        public IActionResult Create()
        {
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "ChurchName");
            return View();
        }

        // POST: VBS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EndDate,StartDate,TenantId,ThemeName")] VBS vBS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vBS);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "ChurchName", vBS.TenantId);
            return View(vBS);
        }

        // GET: VBS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vBS = await _context.VBS.SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,EndDate,StartDate,TenantId,ThemeName")] VBS vBS)
        {
            if (id != vBS.Id)
            {
                return NotFound();
            }

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
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "ChurchName", vBS.TenantId);
            return View(vBS);
        }

        // GET: VBS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vBS = await _context.VBS.SingleOrDefaultAsync(m => m.Id == id);
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
            var vBS = await _context.VBS.SingleOrDefaultAsync(m => m.Id == id);
            _context.VBS.Remove(vBS);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VBSExists(int id)
        {
            return _context.VBS.Any(e => e.Id == id);
        }
    }
}
