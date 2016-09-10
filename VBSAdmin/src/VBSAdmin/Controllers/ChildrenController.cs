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
    public class ChildrenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChildrenController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Children
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Children.Include(c => c.Class).Include(c => c.Guardian).Include(c => c.Session).Include(c => c.VBS);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.SingleOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["GuardianId"] = new SelectList(_context.Guardians, "Id", "Address1");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["VBSId"] = new SelectList(_context.VBS, "Id", "ThemeName");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AllergiesDescription,ClassId,CreatedDate,DateOfBirth,DecisionMade,FirstName,Gender,GradeCompleted,GuardianId,HasAllergies,HasMedicalCondition,HasMedications,LastName,MedicalConditionDescription,MedicationDescription,PlaceChildWithRequest,SessionId,Timestamp,VBSId")] Child child)
        {
            if (ModelState.IsValid)
            {
                _context.Add(child);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", child.ClassId);
            ViewData["GuardianId"] = new SelectList(_context.Guardians, "Id", "Address1", child.GuardianId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", child.SessionId);
            ViewData["VBSId"] = new SelectList(_context.VBS, "Id", "ThemeName", child.VBSId);
            return View(child);
        }

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.SingleOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", child.ClassId);
            ViewData["GuardianId"] = new SelectList(_context.Guardians, "Id", "Address1", child.GuardianId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", child.SessionId);
            ViewData["VBSId"] = new SelectList(_context.VBS, "Id", "ThemeName", child.VBSId);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AllergiesDescription,ClassId,CreatedDate,DateOfBirth,DecisionMade,FirstName,Gender,GradeCompleted,GuardianId,HasAllergies,HasMedicalCondition,HasMedications,LastName,MedicalConditionDescription,MedicationDescription,PlaceChildWithRequest,SessionId,Timestamp,VBSId")] Child child)
        {
            if (id != child.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(child);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildExists(child.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", child.ClassId);
            ViewData["GuardianId"] = new SelectList(_context.Guardians, "Id", "Address1", child.GuardianId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", child.SessionId);
            ViewData["VBSId"] = new SelectList(_context.VBS, "Id", "ThemeName", child.VBSId);
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.SingleOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var child = await _context.Children.SingleOrDefaultAsync(m => m.Id == id);
            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChildExists(int id)
        {
            return _context.Children.Any(e => e.Id == id);
        }
    }
}
