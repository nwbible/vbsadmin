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

namespace VBSAdmin.Controllers
{
    [Authorize(Policy = "BelongToTenant")]
    public class ClassroomsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ClassroomsController(ApplicationDbContext context) : base(context)
        {
            _context = context;    
        }

        // GET: Classrooms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Classes.Include(c => c.Session)
                .Include(c => c.VBS)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.Grade);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classes.Include(c => c.Session).Where(c => c.Id == id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId).SingleOrDefaultAsync();
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // GET: Classrooms/Create
        public IActionResult Create()
        {
            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period");
            return View();
        }

        // POST: Classrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gender,Grade,Name,SessionId")] Classroom classroom)
        {
            classroom.VBSId = this.CurrentVBSId;

            if (ModelState.IsValid)
            {
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", classroom.SessionId);
            return View(classroom);
        }

        // GET: Classrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classes.Where(c => c.Id == id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId).SingleOrDefaultAsync();
            if (classroom == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", classroom.SessionId);
            return View(classroom);
        }

        // POST: Classrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Gender,Grade,Name,SessionId")] Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return NotFound();
            }

            classroom.VBSId = this.CurrentVBSId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomExists(classroom.Id))
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
            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", classroom.SessionId);
            return View(classroom);
        }

        // GET: Classrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classes
                .Include(c => c.Session)
                .Where(c => c.Id == id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .SingleOrDefaultAsync();

            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // POST: Classrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classroom = await _context.Classes.Where(c => c.Id == id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId).SingleOrDefaultAsync();
            _context.Classes.Remove(classroom);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ClassroomExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
