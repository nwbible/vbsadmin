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
using VBSAdmin.Models.ClassroomViewModels;

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
                .Include(c => c.Children)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.Grade);

            //TODO: Use AutoMapper
            List<Classroom> dbClassrooms = await applicationDbContext.ToListAsync();
            List<IndexViewModel> classRoomVMs = new List<IndexViewModel>();

            foreach(Classroom dbClass in dbClassrooms)
            {
                IndexViewModel vm = new IndexViewModel();
                vm.Grade = dbClass.Grade;
                vm.Id = dbClass.Id;
                vm.Name = dbClass.Name;
                vm.Session = dbClass.Session;
                vm.SessionId = dbClass.SessionId;
                vm.ChildCount = dbClass.Children.Count;

                classRoomVMs.Add(vm);
            }

            return View(classRoomVMs);
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

            //Only support mixed gender classrooms for now
            classroom.Gender = Enums.ClassGender.Mixed;

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

            //Only support mixed gender classrooms for now
            classroom.Gender = Enums.ClassGender.Mixed;

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

        // GET: Classroom Rosters with Name Only
        public async Task<IActionResult> RostersName()
        {
            var applicationDbContext = _context.Classes.Include(c => c.Session)
                .Include(c => c.Children)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name);

            //TODO: Use AutoMapper
            List<Classroom> dbClassrooms = await applicationDbContext.ToListAsync();
            List<RostersNameViewModel> rosterNameVMs = new List<RostersNameViewModel>();

            foreach (Classroom dbClass in dbClassrooms)
            {
                RostersNameViewModel vm = new RostersNameViewModel();
                vm.Grade = dbClass.Grade;
                vm.Id = dbClass.Id;
                vm.Name = dbClass.Name;
                vm.SessionPeriod = dbClass.Session.Period;
                vm.ChildrenNames = new List<string>();

                List<Child> sortedChildren = dbClass.Children.OrderBy(c => c.LastName).ToList();
                foreach(Child child in sortedChildren)
                {
                    vm.ChildrenNames.Add(child.LastName + ", " + child.FirstName);
                }
                

                rosterNameVMs.Add(vm);
            }

            return View(rosterNameVMs);
        }

        // GET: Classroom Rosters with Address
        public async Task<IActionResult> RostersAddress()
        {
            var applicationDbContext = _context.Classes.Include(c => c.Session)
                .Include(c => c.Children)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name);

            //TODO: Use AutoMapper
            List<Classroom> dbClassrooms = await applicationDbContext.ToListAsync();
            List<RostersAddressViewModel> rosterAddressVMs = new List<RostersAddressViewModel>();

            foreach (Classroom dbClass in dbClassrooms)
            {
                RostersAddressViewModel vm = new RostersAddressViewModel();
                vm.Grade = dbClass.Grade;
                vm.Id = dbClass.Id;
                vm.Name = dbClass.Name;
                vm.SessionPeriod = dbClass.Session.Period;
                vm.Children = new List<RostersAddressChildrenViewModel>();

                List<Child> sortedChildren = dbClass.Children.OrderBy(c => c.LastName).ToList();
                foreach (Child child in sortedChildren)
                {
                    RostersAddressChildrenViewModel childVM = new RostersAddressChildrenViewModel();
                    childVM.Name = child.LastName + ", " + child.FirstName;
                    string address = child.Address1;
                    if(!string.IsNullOrEmpty(child.Address2))
                    {
                        address = address + ", " + child.Address2;
                    }
                    childVM.Address = address;
                    childVM.City = child.City;
                    childVM.State = child.State;
                    childVM.Zip = child.Zip;
                    vm.Children.Add(childVM);
                }


                rosterAddressVMs.Add(vm);
            }

            return View(rosterAddressVMs);
        }


        // GET: Classroom Rosters with Allergies
        public async Task<IActionResult> RostersAllergies(string session)
        {
            Enums.SessionPeriod sessionPeriod = Enums.SessionPeriod.AM;

            if(!string.IsNullOrEmpty(session) && session.ToUpper() == "PM")
            {
                sessionPeriod = Enums.SessionPeriod.PM;
            }

            var applicationDbContext = _context.Classes.Include(c => c.Session)
                .Include(c => c.Children)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId && c.Session.Period == sessionPeriod)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name);

            //TODO: Use AutoMapper
            List<Classroom> dbClassrooms = await applicationDbContext.ToListAsync();
            List<RostersAllergiesViewModel> rosterAllergiesVMs = new List<RostersAllergiesViewModel>();

            foreach (Classroom dbClass in dbClassrooms)
            {
                RostersAllergiesViewModel vm = new RostersAllergiesViewModel();
                vm.Grade = dbClass.Grade;
                vm.Id = dbClass.Id;
                vm.Name = dbClass.Name;
                vm.SessionPeriod = dbClass.Session.Period;
                vm.Children = new List<RostersAllergiesChildrenViewModel>();

                List<Child> sortedChildren = dbClass.Children.OrderBy(c => c.LastName).ToList();
                foreach (Child child in sortedChildren)
                {
                    if (child.HasAllergies || !string.IsNullOrEmpty(child.AllergiesDescription))
                    {
                        RostersAllergiesChildrenViewModel childVM = new RostersAllergiesChildrenViewModel();
                        childVM.Name = child.LastName + ", " + child.FirstName;
                        childVM.AllergyDescription = child.AllergiesDescription;
                        vm.Children.Add(childVM);
                    }
                }


                rosterAllergiesVMs.Add(vm);
            }

            return View(rosterAllergiesVMs);
        }


        // GET: Classroom Rosters with Medical Conditions and Medications
        public async Task<IActionResult> RostersMedical(string session)
        {
            Enums.SessionPeriod sessionPeriod = Enums.SessionPeriod.AM;

            if (!string.IsNullOrEmpty(session) && session.ToUpper() == "PM")
            {
                sessionPeriod = Enums.SessionPeriod.PM;
            }

            var applicationDbContext = _context.Classes.Include(c => c.Session)
                .Include(c => c.Children)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId && c.Session.Period == sessionPeriod)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name);

            //TODO: Use AutoMapper
            List<Classroom> dbClassrooms = await applicationDbContext.ToListAsync();
            List<RostersMedicalViewModel> rosterMedicalVMs = new List<RostersMedicalViewModel>();

            foreach (Classroom dbClass in dbClassrooms)
            {
                RostersMedicalViewModel vm = new RostersMedicalViewModel();
                vm.Grade = dbClass.Grade;
                vm.Id = dbClass.Id;
                vm.Name = dbClass.Name;
                vm.SessionPeriod = dbClass.Session.Period;
                vm.Children = new List<RostersMedicalChildrenViewModel>();

                List<Child> sortedChildren = dbClass.Children.OrderBy(c => c.LastName).ToList();
                foreach (Child child in sortedChildren)
                {
                    if (child.HasMedicalCondition || 
                        !string.IsNullOrEmpty(child.MedicalConditionDescription) ||
                        child.HasMedications ||
                        !string.IsNullOrEmpty(child.MedicationDescription))
                    {
                        RostersMedicalChildrenViewModel childVM = new RostersMedicalChildrenViewModel();
                        childVM.Name = child.LastName + ", " + child.FirstName;
                        childVM.MedicalConditionDescription = child.MedicalConditionDescription;
                        childVM.MedicinesDescription = child.MedicationDescription;
                        vm.Children.Add(childVM);
                    }
                }


                rosterMedicalVMs.Add(vm);
            }

            return View(rosterMedicalVMs);
        }


        private bool ClassroomExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
