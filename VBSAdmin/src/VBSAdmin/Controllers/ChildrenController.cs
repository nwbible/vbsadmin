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
    public class ChildrenController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ChildrenController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Children
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Children.Include(c => c.Classroom)
                .Include(c => c.Session)
                .Include(c => c.VBS)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.Id);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .Include(c => c.Session)
                .Include(c => c.Classroom)
                .Where(c => c.Id == id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .SingleOrDefaultAsync();

            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            ViewData["ClassroomId"] = new SelectList(_context.Classes.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address1,Address2,AllergiesDescription,AttendHostChurch,City,ClassroomId,DateOfBirth,DecisionMade,EmergencyContactFirstName,EmergencyContactLastName,EmergencyContactPhone,FirstName,Gender,GradeCompleted,GuardianChildRelationship,GuardianEmail,GuardianFirstName,GuardianLastName,GuardianPhone,HasAllergies,HasMedicalCondition,HasMedications,HomeChurch,InvitedBy,LastName,MedicalConditionDescription,MedicationDescription,PlaceChildWithRequest,SessionId,State,Zip")] Child child)
        {
            child.VBSId = this.CurrentVBSId;
            child.DateOfRegistration = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                _context.Add(child);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classes.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Name", child.ClassroomId);
            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", child.SessionId);
            return View(child);
        }

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .Where(c => c.Id == id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .SingleOrDefaultAsync();

            if (child == null)
            {
                return NotFound();
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classes.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Name", child.ClassroomId);
            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", child.SessionId);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address1,Address2,AllergiesDescription,AttendHostChurch,City,ClassroomId,DateOfBirth,DecisionMade,EmergencyContactFirstName,EmergencyContactLastName,EmergencyContactPhone,FirstName,Gender,GradeCompleted,GuardianChildRelationship,GuardianEmail,GuardianFirstName,GuardianLastName,GuardianPhone,HasAllergies,HasMedicalCondition,HasMedications,HomeChurch,InvitedBy,LastName,MedicalConditionDescription,MedicationDescription,PlaceChildWithRequest,SessionId,State,Zip")] Child child)
        {
            if (id != child.Id)
            {
                return NotFound();
            }

            child.VBSId = this.CurrentVBSId;

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
            ViewData["ClassroomId"] = new SelectList(_context.Classes.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Name", child.ClassroomId);
            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", child.SessionId);
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .Include(c => c.Session)
                .Include(c => c.Classroom)
                .Where(c => c.Id == id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .SingleOrDefaultAsync();

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
            var child = await _context.Children
                .Where(c => c.Id == id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .SingleOrDefaultAsync();

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
