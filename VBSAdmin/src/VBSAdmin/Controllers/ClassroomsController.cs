using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VBSAdmin.Data;
using VBSAdmin.Models.VBSAdminModels;

namespace VBSAdmin
{
    public class ClassroomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassroomsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Classrooms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Classes.Include(c => c.Session).Include(c => c.VBS);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classes.SingleOrDefaultAsync(m => m.Id == id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // GET: Classrooms/Create
        public IActionResult Create()
        {
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Period");
            ViewData["VBSId"] = new SelectList(_context.VBS, "Id", "ThemeName");
            return View();
        }

        // POST: Classrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gender,Grade,Name,SessionId,VBSId")] Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Period", classroom.SessionId);
            ViewData["VBSId"] = new SelectList(_context.VBS, "Id", "ThemeName", classroom.VBSId);
            return View(classroom);
        }

        // GET: Classrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classes.SingleOrDefaultAsync(m => m.Id == id);
            if (classroom == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Period", classroom.SessionId);
            ViewData["VBSId"] = new SelectList(_context.VBS, "Id", "ThemeName", classroom.VBSId);
            return View(classroom);
        }

        // POST: Classrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Gender,Grade,Name,SessionId,VBSId")] Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return NotFound();
            }

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
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Period", classroom.SessionId);
            ViewData["VBSId"] = new SelectList(_context.VBS, "Id", "ThemeName", classroom.VBSId);
            return View(classroom);
        }

        // GET: Classrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classes.SingleOrDefaultAsync(m => m.Id == id);
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
            var classroom = await _context.Classes.SingleOrDefaultAsync(m => m.Id == id);
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
