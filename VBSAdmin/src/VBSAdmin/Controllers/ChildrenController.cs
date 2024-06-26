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
using VBSAdmin.Helpers;

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
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName);

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

            DetailsChildViewModel childVM = new DetailsChildViewModel
            {
                Id = child.Id,
                Address1 = child.Address1,
                Address2 = child.Address2,
                AllergiesDescription = child.AllergiesDescription,
                AttendHostChurch = child.AttendHostChurch,
                City = child.City,
                ClassroomName = (child.Classroom != null) ? child.Classroom.Session.Period + " " + child.Classroom.Grade.GetDisplayName() + " " + child.Classroom.Name : "",
                DateOfBirth = child.DateOfBirth,
                DecisionMade = child.DecisionMade,
                EmergencyContactName = child.EmergencyContactFirstName + " " + child.EmergencyContactLastName,
                EmergencyContactPhone = child.EmergencyContactPhone,
                EmergencyContactChildRelationship = child.EmergencyContactChildRelationship,
                Gender = child.Gender,
                GradeCompleted = child.GradeCompleted,
                GuardianChildRelationship = child.GuardianChildRelationship,
                GuardianEmail = child.GuardianEmail,
                GuardianName = child.GuardianFirstName + " " + child.GuardianLastName,
                GuardianPhone = child.GuardianPhone,
                HomeChurch = child.HomeChurch,
                InvitedBy = child.InvitedBy,
                MedicalConditionDescription = child.MedicalConditionDescription,
                MedicationDescription = child.MedicationDescription,
                Name = child.FirstName + " " + child.LastName,
                Period = child.Session.Period,
                PlaceChildWithRequest = child.PlaceChildWithRequest,
                State = child.State,
                Zip = child.Zip
            };

            return View(childVM);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            CreateChildViewModel createChildVM = new CreateChildViewModel();

            List<Classroom> classrooms = _context.Classes
                .Include(c => c.Session)
                .Where(c => c.VBSId == this.CurrentVBSId)
                .OrderBy(c => c.Session.Period)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name)
                .ToList();

            SelectListItem noneClassSelectItem = new SelectListItem { Value = "0", Text = "None", Selected = true };
            List<SelectListItem> assignClassrooms = new List<SelectListItem>();
            assignClassrooms.Add(noneClassSelectItem);

            foreach (Classroom dbClass in classrooms)
            {
                SelectListItem assignClass = new SelectListItem();
                assignClass.Value = dbClass.Id.ToString();
                assignClass.Text = dbClass.Session.Period + " " + dbClass.Grade.GetDisplayName() + " " + dbClass.Name;
                assignClassrooms.Add(assignClass);
            }

            createChildVM.ClassroomSelectItems = assignClassrooms;

            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period");
            return View(createChildVM);
        }

        // POST: Children/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address1,Address2,AllergiesDescription,AttendHostChurch,City,ClassroomId,DateOfBirth,DecisionMade,EmergencyContactFirstName,EmergencyContactLastName,EmergencyContactPhone,EmergencyContactChildRelationship,FirstName,Gender,GradeCompleted,GuardianChildRelationship,GuardianEmail,GuardianFirstName,GuardianLastName,GuardianPhone,HomeChurch,InvitedBy,LastName,MedicalConditionDescription,MedicationDescription,PlaceChildWithRequest,SessionId,State,Zip")] CreateChildViewModel childVM)
        {
            if (ModelState.IsValid)
            {
                Child child = new Child
                {
                    VBSId = this.CurrentVBSId,
                    DateOfRegistration = DateTime.UtcNow,
                    Address1 = childVM.Address1,
                    Address2 = childVM.Address2,
                    AllergiesDescription = childVM.AllergiesDescription,
                    AttendHostChurch = childVM.AttendHostChurch,
                    City = childVM.City,
                    ClassroomId = childVM.ClassroomId,
                    DateOfBirth = childVM.DateOfBirth,
                    DecisionMade = childVM.DecisionMade,
                    EmergencyContactFirstName = childVM.EmergencyContactFirstName,
                    EmergencyContactLastName = childVM.EmergencyContactLastName,
                    EmergencyContactPhone = childVM.EmergencyContactPhone,
                    EmergencyContactChildRelationship = childVM.EmergencyContactChildRelationship,
                    FirstName = childVM.FirstName,
                    Gender = childVM.Gender,
                    GradeCompleted = childVM.GradeCompleted,
                    GuardianChildRelationship = childVM.GuardianChildRelationship,
                    GuardianEmail = childVM.GuardianEmail,
                    GuardianFirstName = childVM.GuardianFirstName,
                    GuardianLastName = childVM.GuardianLastName,
                    GuardianPhone = childVM.GuardianPhone,
                    HomeChurch = childVM.HomeChurch,
                    InvitedBy = childVM.InvitedBy,
                    HasAllergies = false,
                    HasMedicalCondition = false,
                    HasMedications = false,
                    LastName = childVM.LastName,
                    MedicalConditionDescription = childVM.MedicalConditionDescription,
                    MedicationDescription = childVM.MedicationDescription,
                    PlaceChildWithRequest = childVM.PlaceChildWithRequest,
                    SessionId = childVM.SessionId,
                    State = childVM.State,
                    Zip = childVM.Zip
                };

                GetGeoCodeResponse geoResponse = await GeocodeHelper.GetGeoCode(child);
                if(geoResponse != null)
                {
                    child.Latitude = geoResponse.Lat;
                    child.Longitude = geoResponse.Long;
                }

                if(child.ClassroomId == 0)
                {
                    child.ClassroomId = null;
                }

                _context.Add(child);
                await _context.SaveChangesAsync();
                childVM.Id = child.Id;
                return RedirectToAction("Index");
            }

            List<Classroom> classrooms = _context.Classes
                .Include(c => c.Session)
                .Where(c => c.VBSId == this.CurrentVBSId)
                .OrderBy(c => c.Session.Period)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name)
                .ToList();

            SelectListItem noneClassSelectItem = new SelectListItem { Value = "0", Text = "None", Selected = true };
            List<SelectListItem> assignClassrooms = new List<SelectListItem>();
            assignClassrooms.Add(noneClassSelectItem);

            foreach (Classroom dbClass in classrooms)
            {
                SelectListItem assignClass = new SelectListItem();
                assignClass.Value = dbClass.Id.ToString();
                assignClass.Text = dbClass.Session.Period + " " + dbClass.Grade.GetDisplayName() + " " + dbClass.Name;
                assignClassrooms.Add(assignClass);
            }

            childVM.ClassroomSelectItems = assignClassrooms;

            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", childVM.SessionId);
            return View(childVM);
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

            EditChildViewModel editVM = new EditChildViewModel
            {
                Id = child.Id,
                Address1 = child.Address1,
                Address2 = child.Address2,
                AllergiesDescription = child.AllergiesDescription,
                AttendHostChurch = child.AttendHostChurch,
                City = child.City,
                ClassroomId = child.ClassroomId,
                DateOfBirth = child.DateOfBirth,
                DecisionMade = child.DecisionMade,
                EmergencyContactFirstName = child.EmergencyContactFirstName,
                EmergencyContactLastName = child.EmergencyContactLastName,
                EmergencyContactPhone = child.EmergencyContactPhone,
                EmergencyContactChildRelationship = child.EmergencyContactChildRelationship,
                FirstName = child.FirstName,
                Gender = child.Gender,
                GradeCompleted = child.GradeCompleted,
                GuardianChildRelationship = child.GuardianChildRelationship,
                GuardianEmail = child.GuardianEmail,
                GuardianFirstName = child.GuardianFirstName,
                GuardianLastName = child.GuardianLastName,
                GuardianPhone = child.GuardianPhone,
                HomeChurch = child.HomeChurch,
                InvitedBy = child.InvitedBy,
                LastName = child.LastName,
                MedicalConditionDescription = child.MedicalConditionDescription,
                MedicationDescription = child.MedicationDescription,
                PlaceChildWithRequest = child.PlaceChildWithRequest,
                SessionId = child.SessionId,
                State = child.State,
                Zip = child.Zip
            };

            List<Classroom> classrooms = _context.Classes
                .Include(c => c.Session)
                .Where(c => c.VBSId == this.CurrentVBSId)
                .OrderBy(c => c.Session.Period)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name)
                .ToList();

            SelectListItem noneClassSelectItem = new SelectListItem { Value = "0", Text = "None", Selected = true };
            List<SelectListItem> assignClassrooms = new List<SelectListItem>();
            assignClassrooms.Add(noneClassSelectItem);

            foreach (Classroom dbClass in classrooms)
            {
                SelectListItem assignClass = new SelectListItem();
                assignClass.Value = dbClass.Id.ToString();
                assignClass.Text = dbClass.Session.Period + " " + dbClass.Grade.GetDisplayName() + " " + dbClass.Name;
                assignClassrooms.Add(assignClass);
            }

            editVM.ClassroomSelectItems = assignClassrooms;

            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", child.SessionId);
            return View(editVM);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address1,Address2,AllergiesDescription,AttendHostChurch,City,ClassroomId,DateOfBirth,DecisionMade,EmergencyContactFirstName,EmergencyContactLastName,EmergencyContactPhone,EmergencyContactChildRelationship,FirstName,Gender,GradeCompleted,GuardianChildRelationship,GuardianEmail,GuardianFirstName,GuardianLastName,GuardianPhone,HasAllergies,HasMedicalCondition,HasMedications,HomeChurch,InvitedBy,LastName,MedicalConditionDescription,MedicationDescription,PlaceChildWithRequest,SessionId,State,Zip")] EditChildViewModel childVM)
        {
            if (id != childVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var child = await _context.Children
                    .Where(c => c.Id == childVM.Id && c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                    .SingleOrDefaultAsync();

                child.VBSId = this.CurrentVBSId;
                child.Address1 = childVM.Address1;
                child.Address2 = childVM.Address2;
                child.AllergiesDescription = childVM.AllergiesDescription;
                child.AttendHostChurch = childVM.AttendHostChurch;
                child.City = childVM.City;
                child.ClassroomId = childVM.ClassroomId;
                child.DateOfBirth = childVM.DateOfBirth;
                child.DecisionMade = childVM.DecisionMade;
                child.EmergencyContactFirstName = childVM.EmergencyContactFirstName;
                child.EmergencyContactLastName = childVM.EmergencyContactLastName;
                child.EmergencyContactPhone = childVM.EmergencyContactPhone;
                child.EmergencyContactChildRelationship = childVM.EmergencyContactChildRelationship;
                child.FirstName = childVM.FirstName;
                child.Gender = childVM.Gender;
                child.GradeCompleted = childVM.GradeCompleted;
                child.GuardianChildRelationship = childVM.GuardianChildRelationship;
                child.GuardianEmail = childVM.GuardianEmail;
                child.GuardianFirstName = childVM.GuardianFirstName;
                child.GuardianLastName = childVM.GuardianLastName;
                child.GuardianPhone = childVM.GuardianPhone;
                child.HomeChurch = childVM.HomeChurch;
                child.InvitedBy = childVM.InvitedBy;
                child.HasAllergies = false;
                child.HasMedicalCondition = false;
                child.HasMedications = false;
                child.LastName = childVM.LastName;
                child.MedicalConditionDescription = childVM.MedicalConditionDescription;
                child.MedicationDescription = childVM.MedicationDescription;
                child.PlaceChildWithRequest = childVM.PlaceChildWithRequest;
                child.SessionId = childVM.SessionId;
                child.State = childVM.State;
                child.Zip = childVM.Zip;

                GetGeoCodeResponse geoResponse = await GeocodeHelper.GetGeoCode(child);
                if (geoResponse != null)
                {
                    child.Latitude = geoResponse.Lat;
                    child.Longitude = geoResponse.Long;
                }


                if (child.ClassroomId == 0)
                {
                    child.ClassroomId = null;
                }

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

            List<Classroom> classrooms = _context.Classes
                .Include(c => c.Session)
                .Where(c => c.VBSId == this.CurrentVBSId)
                .OrderBy(c => c.Session.Period)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name)
                .ToList();

            SelectListItem noneClassSelectItem = new SelectListItem { Value = "0", Text = "None", Selected = true };
            List<SelectListItem> assignClassrooms = new List<SelectListItem>();
            assignClassrooms.Add(noneClassSelectItem);

            foreach (Classroom dbClass in classrooms)
            {
                SelectListItem assignClass = new SelectListItem();
                assignClass.Value = dbClass.Id.ToString();
                assignClass.Text = dbClass.Session.Period + " " + dbClass.Grade.GetDisplayName() + " " + dbClass.Name;
                assignClassrooms.Add(assignClass);
            }

            childVM.ClassroomSelectItems = assignClassrooms;

            ViewData["SessionId"] = new SelectList(_context.Sessions.Where(s => s.VBSId == this.CurrentVBSId), "Id", "Period", childVM.SessionId);
            return View(childVM);
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


        // GET: Alphabetical listing of chidren and class assignments by session
        public async Task<IActionResult> AlphabeticalClassAssignments(string session)
        {
            Enums.SessionPeriod sessionPeriod = Enums.SessionPeriod.AM;

            if (!string.IsNullOrEmpty(session) && session.ToUpper() == "PM")
            {
                sessionPeriod = Enums.SessionPeriod.PM;
            }

            var applicationDbContext = _context.Children.Include(c => c.Session)
                .Include(c => c.Classroom)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId && c.Session.Period == sessionPeriod)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName);

            //TODO: Use AutoMapper
            List<Child> dbChildren = await applicationDbContext.ToListAsync();
            List<AlphabeticalClassAssignmentsViewModel> alphaAssignmentVMs = new List<AlphabeticalClassAssignmentsViewModel>();

            foreach (Child dbChild in dbChildren)
            {
                AlphabeticalClassAssignmentsViewModel vm = new AlphabeticalClassAssignmentsViewModel();
                vm.Name = dbChild.LastName + ", " + dbChild.FirstName;
                if (dbChild.Classroom != null)
                {
                    vm.ClassAssignment = dbChild.Session.Period + " " + dbChild.Classroom.Grade.GetDisplayName() + " " + dbChild.Classroom.Name;
                }
                else
                {
                    vm.ClassAssignment = "Not assigned to classroom";
                }

                alphaAssignmentVMs.Add(vm);
            }

            return View(alphaAssignmentVMs);
        }

        // GET: Map of registered children
        public async Task<IActionResult> ChildrenMap()
        {
            var applicationDbContext = _context.Children
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId);

            List<Child> dbChildren = await applicationDbContext.ToListAsync();

            Dictionary<string, string> addressList = new Dictionary<string, string>();
            string markers = "[";

            foreach (Child child in dbChildren)
            {
                string childAddress = GeocodeHelper.GetFullAddress(child);

                if (string.IsNullOrWhiteSpace(child.Latitude) || string.IsNullOrWhiteSpace(child.Longitude))
                {
                    var response = await GeocodeHelper.GetGeoCode(childAddress);
                    if(response != null)
                    {
                        child.Latitude = response.Lat;
                        child.Longitude = response.Long;
                        _context.Update(child);
                        await _context.SaveChangesAsync();
                    }
                }
                

                if (!addressList.ContainsKey(childAddress))
                {
                    addressList.Add(childAddress, child.LastName);

                    string homeChurch;

                    if(child.AttendHostChurch)
                    {
                        homeChurch = "home";
                    }
                    else
                    {
                        if (ChurchHelper.IsNoneChurch(child.HomeChurch))
                        {
                            homeChurch = "none";
                        }
                        else
                        {
                            homeChurch = "other";
                        }
                    }

                    markers += "{";
                    markers += string.Format("'title': '{0}',", System.Net.WebUtility.HtmlEncode(child.LastName));
                    markers += string.Format("'lat': '{0}',", child.Latitude);
                    markers += string.Format("'lng': '{0}',", child.Longitude);
                    markers += string.Format("'description': '{0}',", System.Net.WebUtility.HtmlEncode(childAddress));
                    markers += string.Format("'homeChurch': '{0}'", homeChurch);
                    markers += "},";
                }
            }

            markers += "];";

            ViewBag.Markers = markers;

            ChildrenMapViewModel vm = new ChildrenMapViewModel();

            vm.ChurchName = "Northwest Bible Church";

            return View(vm);
        }


        // GET: Listing of represented churches
        public async Task<IActionResult> RepresentedChurches()
        {
            var applicationDbContext = _context.Children
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.HomeChurch);

            List<Child> dbChildren = await applicationDbContext.ToListAsync();
            RepresentedChurchesViewModel vm = new RepresentedChurchesViewModel();
            vm.Churches = new Dictionary<string, int>();

            foreach (Child dbChild in dbChildren)
            {
                string church = dbChild.HomeChurch;

                if (ChurchHelper.IsNoneChurch(church))
                {
                    church = "None";
                }


                if (dbChild.AttendHostChurch)
                {
                    church = "Northwest Bible Church";
                }


                if(vm.Churches.ContainsKey(church.Trim()))
                {
                    vm.Churches[church.Trim()] = vm.Churches[church.Trim()] + 1;
                }
                else
                {
                    vm.Churches.Add(church.Trim(), 1);
                }

            }

            return View(vm);
        }


        // GET: Listing of registered unchurched children.
        public async Task<IActionResult> UnchurchedReportOld()
        {
            var applicationDbContext = _context.Children
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.GuardianLastName)
                .ThenBy(c => c.Address1)
                .ThenBy(c => c.GradeCompleted);

            List<Child> dbChildren = await applicationDbContext.ToListAsync();
            List<UnchurchedReportViewModel> vms = new List<UnchurchedReportViewModel>();

            foreach (Child dbChild in dbChildren)
            {
                string church = dbChild.HomeChurch;

                if (!dbChild.AttendHostChurch && ChurchHelper.IsNoneChurch(church))
                {
                    UnchurchedReportViewModel vm = new UnchurchedReportViewModel();

                    vm.ChildName = dbChild.LastName + ", " + dbChild.FirstName;
                    vm.GradeCompleted = dbChild.GradeCompleted;
                    vm.Address = dbChild.Address1 + ", ";
                    if(!string.IsNullOrEmpty(dbChild.Address2))
                    {
                        vm.Address += dbChild.Address2 + ", ";
                    }
                    vm.Address += dbChild.City + ", ";
                    vm.Address += dbChild.State + ", ";
                    vm.Address += dbChild.Zip;
                    vm.GuardianName = dbChild.GuardianFirstName + " " + dbChild.GuardianLastName;
                    vm.GuardianRelationship = dbChild.GuardianChildRelationship;
                    vm.GuardianEmail = dbChild.GuardianEmail;
                    vm.GuardianPhone = dbChild.GuardianPhone;
                    vm.ChurchSpecified = dbChild.HomeChurch;
                    vm.InvitedBy = dbChild.InvitedBy;

                    vms.Add(vm);
                }
            }

            return View(vms);
        }

        // GET: Listing of registered unchurched children.
        public async Task<IActionResult> UnchurchedReport()
        {
            var applicationDbContext = _context.Children
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.GuardianLastName)
                .ThenBy(c => c.Address1)
                .ThenBy(c => c.GradeCompleted);

            List<Child> dbChildren = await applicationDbContext.ToListAsync();
            List<UnchurchedReportViewModel2> vms = new List<UnchurchedReportViewModel2>();


            Dictionary<string, UnchurchedReportViewModel2> vmDictionary = new Dictionary<string, UnchurchedReportViewModel2>();

            foreach (Child dbChild in dbChildren)
            {
                string church = dbChild.HomeChurch;

                if (!dbChild.AttendHostChurch && ChurchHelper.IsNoneChurch(church))
                {
                    if (!vmDictionary.ContainsKey(dbChild.GuardianFirstName + " " + dbChild.GuardianLastName))
                    {
                        UnchurchedReportViewModel2 vm = new UnchurchedReportViewModel2();

                        vm.ChildrenNamesAndGrades = dbChild.FirstName + " " + dbChild.LastName + "(" + dbChild.GradeCompleted + ")"; 
                        vm.Address = dbChild.Address1 + ", ";
                        if (!string.IsNullOrEmpty(dbChild.Address2))
                        {
                            vm.Address += dbChild.Address2 + ", ";
                        }
                        vm.Address += dbChild.City + ", ";
                        vm.Address += dbChild.State + ", ";
                        vm.Address += dbChild.Zip;
                        vm.GuardianName = dbChild.GuardianFirstName + " " + dbChild.GuardianLastName;
                        vm.GuardianRelationship = dbChild.GuardianChildRelationship;
                        vm.GuardianEmail = dbChild.GuardianEmail;
                        vm.GuardianPhone = dbChild.GuardianPhone;
                        vm.ChurchSpecified = dbChild.HomeChurch;
                        vm.InvitedBy = dbChild.InvitedBy;

                        vmDictionary.Add(vm.GuardianName, vm);
                    }
                    else
                    {
                        UnchurchedReportViewModel2 vm = vmDictionary[dbChild.GuardianFirstName + " " + dbChild.GuardianLastName];

                        vm.ChildrenNamesAndGrades+= ", " + dbChild.FirstName + " " + dbChild.LastName + "(" + dbChild.GradeCompleted + ")";
                    }
                }
            }

            foreach (UnchurchedReportViewModel2 vm2 in vmDictionary.Values)
            {
                vms.Add(vm2);
            }

            return View(vms);
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
                assignClass.Text = dbAMClass.Session.Period + " " + dbAMClass.Grade.GetDisplayName() + " " + dbAMClass.Name;

                assignAMClassrooms.Add(assignClass);
            }

            model.AMClassroomSelectItems = assignAMClassrooms;

            List<SelectListItem> assignPMClassrooms = new List<SelectListItem>();
            assignPMClassrooms.Add(noneClassSelectItem);

            foreach (Classroom dbPMClass in dbPMClassrooms)
            {
                SelectListItem assignClass = new SelectListItem();
                assignClass.Value = dbPMClass.Id.ToString();
                assignClass.Text = dbPMClass.Session.Period + " " + dbPMClass.Grade.GetDisplayName() + " " + dbPMClass.Name;

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
                //applicationDbContext = applicationDbContext.Where(c => (int)c.GradeCompleted == (int)model.FilterGrade);
                int temp = (int)model.FilterGrade;
                
                applicationDbContext = applicationDbContext.Where(c => (int)c.GradeCompleted == temp);
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
                    assignChild.CurrentClassName = dbChild.Session.Period + " " + dbChild.Classroom.Grade.GetDisplayName() + " " + dbChild.Classroom.Name;
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
                assignChild.HealthConcernsMarkup = string.Empty;
                if(!string.IsNullOrWhiteSpace(dbChild.AllergiesDescription))
                {
                    assignChild.HealthConcernsMarkup = "<div style='text-align:left'><b>Allergies:</b><br/>" + dbChild.AllergiesDescription;
                }

                if(!string.IsNullOrWhiteSpace(dbChild.MedicalConditionDescription))
                {
                    if(!string.IsNullOrWhiteSpace(assignChild.HealthConcernsMarkup))
                    {
                        assignChild.HealthConcernsMarkup += "<br/><br/>";
                    }
                    else
                    {
                        assignChild.HealthConcernsMarkup += "<div style='text-align:left'>";
                    }

                    assignChild.HealthConcernsMarkup += "<b>Medical Conditions:</b><br/>" + dbChild.MedicalConditionDescription;
                }

                if(!string.IsNullOrWhiteSpace(assignChild.HealthConcernsMarkup))
                {
                    assignChild.HealthConcernsMarkup += "</div>";
                }

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
