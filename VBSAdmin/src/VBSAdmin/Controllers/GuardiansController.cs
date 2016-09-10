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
    public class GuardiansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuardiansController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Guardians
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guardians.ToListAsync());
        }

        // GET: Guardians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardians.SingleOrDefaultAsync(m => m.Id == id);
            if (guardian == null)
            {
                return NotFound();
            }

            return View(guardian);
        }

        // GET: Guardians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guardians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address1,Address2,AttendHostChurch,ChildRelationship,City,CreatedDate,Email,EmergencyContact,FirstName,HomeChurch,InvitedBy,LastName,PrimaryPhone,SecondaryPhone,SessionId,State,Timestamp,VBSId,Zip")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guardian);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(guardian);
        }

        // GET: Guardians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardians.SingleOrDefaultAsync(m => m.Id == id);
            if (guardian == null)
            {
                return NotFound();
            }
            return View(guardian);
        }

        // POST: Guardians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address1,Address2,AttendHostChurch,ChildRelationship,City,CreatedDate,Email,EmergencyContact,FirstName,HomeChurch,InvitedBy,LastName,PrimaryPhone,SecondaryPhone,SessionId,State,Timestamp,VBSId,Zip")] Guardian guardian)
        {
            if (id != guardian.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guardian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuardianExists(guardian.Id))
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
            return View(guardian);
        }

        // GET: Guardians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardians.SingleOrDefaultAsync(m => m.Id == id);
            if (guardian == null)
            {
                return NotFound();
            }

            return View(guardian);
        }

        // POST: Guardians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guardian = await _context.Guardians.SingleOrDefaultAsync(m => m.Id == id);
            _context.Guardians.Remove(guardian);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GuardianExists(int id)
        {
            return _context.Guardians.Any(e => e.Id == id);
        }
    }
}
