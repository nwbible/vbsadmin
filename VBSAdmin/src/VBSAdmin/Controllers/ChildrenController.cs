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
using VBSAdmin.Models.ChildrenViewModels;
using VBSAdmin.Filters;

namespace VBSAdmin.Controllers
{
    [Authorize(Policy = "BelongToTenant")]
    public class ChildrenController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ChildrenController(ApplicationDbContext context) : base(context)
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

        // GET: Children/AssignByGrade
        public async Task<IActionResult> Assign(string session)
        {
            Enums.SessionPeriod sessionPeriod = Enums.SessionPeriod.AM;

            if (!string.IsNullOrEmpty(session) && session.ToUpper() == "PM")
            {
                sessionPeriod = Enums.SessionPeriod.PM;
            }

            AssignViewModel assignByGradeViewModel = new Models.ChildrenViewModels.AssignViewModel();

            //Set the default filter option values
            SetDefaultFilterOptions(assignByGradeViewModel);
            assignByGradeViewModel.SessionOption = sessionPeriod;

            //Get the possible class assignments
            await GetPossibleClassrooms(assignByGradeViewModel);

            //Get the children
            await GetChildrenForAssignment(assignByGradeViewModel);

            return View(assignByGradeViewModel);
        }

        // POST: Children/Assign
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestFormSizeLimit(valueCountLimit: 4000)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(AssignViewModel assignViewModel, string ApplyFilters, string Update)
        {
            if (ModelState.IsValid)
            {
                AssignViewModel newAssignVM = new AssignViewModel();
                newAssignVM.FilterGrade = assignViewModel.FilterGrade;
                newAssignVM.FilterName = assignViewModel.FilterName;
                newAssignVM.AssignmentOption = assignViewModel.AssignmentOption;
                newAssignVM.SessionOption = assignViewModel.SessionOption;

                //Get the possible class assignments
                await GetPossibleClassrooms(newAssignVM);

                //The user selected to apply filter children
                if (!string.IsNullOrWhiteSpace(ApplyFilters))
                {
                    //Get the children
                    await GetChildrenForAssignment(newAssignVM);

                    return View(newAssignVM);
                }

                //The user has made updates on the form and is now saving the changes
                if (!string.IsNullOrWhiteSpace(Update))
                {
                    //foreach(AssignChild child in assignViewModel.Children)
                    foreach (ClassAssignment assignment in assignViewModel.Assignments)
                    {
                        if (assignment.CurrentClassId != assignment.NewClassId)
                        {
                            var dbChild = await _context.Children
                                .Where(c => c.Id == assignment.ChildId && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                                .SingleOrDefaultAsync();

                            if (assignment.NewClassId == 0)
                            {
                                dbChild.ClassroomId = null;
                            }
                            else
                            {
                                dbChild.ClassroomId = assignment.NewClassId;
                            }

                            _context.Children.Update(dbChild);
                            await _context.SaveChangesAsync();
                        }
                    }

                    //Get the children
                    await GetChildrenForAssignment(newAssignVM);

                    return View(newAssignVM);
                }

                //Get the children
                await GetChildrenForAssignment(newAssignVM);

                return View(newAssignVM);
            }

            return View(assignViewModel);
        }

        // POST: Children/Search
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string searchText)
        {
            var applicationDbContext = _context.Children.Include(c => c.Classroom)
                .Include(c => c.Session)
                .Include(c => c.VBS)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .Where(c => c.FirstName.Contains(searchText) || c.LastName.Contains(searchText))
                .OrderBy(c => c.Id);

            return View(await applicationDbContext.ToListAsync());
        }



        private bool ChildExists(int id)
        {
            return _context.Children.Any(e => e.Id == id);
        }


        private void SetDefaultFilterOptions(AssignViewModel model)
        {
            model.AssignmentOption = Enums.AssignmentOptions.All;
            model.FilterGrade = Enums.ClassGradeOptions.All;
            model.SessionOption = Enums.SessionPeriod.AM;
            model.FilterName = string.Empty;
        }


        private async Task GetPossibleClassrooms(AssignViewModel model)
        {
            var classesAMDbContext = _context.Classes.Include(c => c.Session)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .Where(c => c.Session.Period == Enums.SessionPeriod.AM)
                .OrderBy(c => c.Id);

            var classesPMDbContext = _context.Classes.Include(c => c.Session)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .Where(c => c.Session.Period == Enums.SessionPeriod.PM)
                .OrderBy(c => c.Id);

            List<Classroom> dbAMClassrooms = await classesAMDbContext.ToListAsync();
            List<Classroom> dbPMClassrooms = await classesPMDbContext.ToListAsync();

            SelectListItem noneClassSelectItem = new SelectListItem { Value = "0", Text = "None"};

            List<SelectListItem> assignAMClassrooms = new List<SelectListItem>();
            assignAMClassrooms.Add(noneClassSelectItem);

            foreach (Classroom dbAMClass in dbAMClassrooms)
            {
                SelectListItem assignClass = new SelectListItem();
                assignClass.Value = dbAMClass.Id.ToString();
                assignClass.Text = dbAMClass.Session.Period + " " + dbAMClass.Grade + " " + dbAMClass.Name;

                assignAMClassrooms.Add(assignClass);
            }

            model.AMClassroomSelectItems = assignAMClassrooms;

            List<SelectListItem> assignPMClassrooms = new List<SelectListItem>();
            assignPMClassrooms.Add(noneClassSelectItem);

            foreach (Classroom dbPMClass in dbPMClassrooms)
            {
                SelectListItem assignClass = new SelectListItem();
                assignClass.Value = dbPMClass.Id.ToString();
                assignClass.Text = dbPMClass.Session.Period + " " + dbPMClass.Grade + " " + dbPMClass.Name;

                assignPMClassrooms.Add(assignClass);
            }

            model.PMClassroomSelectItems = assignPMClassrooms;
        }


        private async Task GetChildrenForAssignment(AssignViewModel model)
        {
            //TODO: Use automapper
            var applicationDbContext = _context.Children.Include(c => c.Classroom)
                .Include(c => c.Session)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId);

            
            applicationDbContext = applicationDbContext.Where(c => c.Session.Period == model.SessionOption);
            
            if(model.AssignmentOption != Enums.AssignmentOptions.All)
            {
                if (model.AssignmentOption == Enums.AssignmentOptions.Assigned)
                {
                    applicationDbContext = applicationDbContext.Where(c => c.ClassroomId > 0);
                }
                else
                {
                    applicationDbContext = applicationDbContext.Where(c => c.ClassroomId == 0 || c.ClassroomId == null);
                }

            }

            if(model.FilterGrade != Enums.ClassGradeOptions.All)
            {
                applicationDbContext = applicationDbContext.Where(c => (int)c.GradeCompleted == (int)model.FilterGrade);
            }

            if(!string.IsNullOrWhiteSpace(model.FilterName))
            {
                applicationDbContext = applicationDbContext.Where(c => c.FirstName.ToLower().Contains(model.FilterName.ToLower()) ||
                    c.LastName.ToLower().Contains(model.FilterName.ToLower()));
            }

            applicationDbContext = applicationDbContext.OrderBy(c => c.Id);

            List<Child> dbChildren = await applicationDbContext.ToListAsync();
            List<AssignChild> assignChildren = new List<AssignChild>();
            List<ClassAssignment> classAssignments = new List<ClassAssignment>();

            foreach (Child dbChild in dbChildren)
            {

                ClassAssignment classAssignment = new ClassAssignment();
                classAssignment.ChildId = dbChild.Id;
                classAssignment.CurrentClassId = (dbChild.ClassroomId.HasValue) ? dbChild.ClassroomId : 0;
                classAssignment.NewClassId = (dbChild.ClassroomId.HasValue) ? dbChild.ClassroomId : 0;
                classAssignments.Add(classAssignment);

                AssignChild assignChild = new AssignChild();
                if (dbChild.Classroom != null)
                {
                    assignChild.CurrentClassName = dbChild.Session.Period + " " + dbChild.Classroom.Grade + " " + dbChild.Classroom.Name;
                }
                else
                {
                    assignChild.CurrentClassName = "None";
                }
                assignChild.DateOfBirth = dbChild.DateOfBirth;
                assignChild.GradeCompleted = dbChild.GradeCompleted;
                assignChild.Id = dbChild.Id;
                assignChild.Name = dbChild.FirstName + " " + dbChild.LastName;
                assignChild.PlaceChildWithRequest = dbChild.PlaceChildWithRequest;
                assignChild.Allergies = dbChild.AllergiesDescription;
                assignChild.MedicalConditions = dbChild.MedicalConditionDescription;
                assignChild.Session = dbChild.Session;
                assignChild.GuardianName = dbChild.GuardianFirstName + " " + dbChild.GuardianLastName;
                assignChild.CurrentClassId = (int)((dbChild.ClassroomId.HasValue) ? dbChild.ClassroomId : 0);
                assignChild.NewClassId = (int)((dbChild.ClassroomId.HasValue) ? dbChild.ClassroomId : 0); assignChildren.Add(assignChild);
            }

            model.Children = assignChildren;
            model.Assignments = classAssignments;
        }
    }
}
