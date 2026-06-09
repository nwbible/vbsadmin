using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VBSAdmin.Data;
using VBSAdmin.Data.VBSAdminModels;
using VBSAdmin.Models.VBSViewModels;
using VBSAdmin.Models.ChildrenViewModels;
using VBSAdmin.Authorization;
using VBSAdmin.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Newtonsoft.Json;

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
        [Authorize(Policy = "TenantAdmin")]
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
            var vBS = await _context.VBS.Include(c => c.Children).Include(s=> s.Sessions).Include(v => v.Tenant).Where(vb => vb.Id == id && vb.TenantId == this.TenantId).SingleOrDefaultAsync();
            if (vBS == null)
            {   
                return NotFound();
            }

            //todo: Use automapper
            DetailsViewModel vm = new DetailsViewModel();
            vm.Id = vBS.Id;
            vm.ThemeName = vBS.ThemeName;
            vm.StartDate = vBS.StartDate;
            vm.EndDate = vBS.EndDate;
            vm.TotalRegisteredChildren = vBS.Children.Count;
            vm.TotalAMRegisteredChildren = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM).Count();
            vm.TotalPMRegisteredChildren = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM).Count();
            vm.TotalAMPreschool = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.PreSchool).Count();
            vm.TotalAMPreK = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.PreK).Count();
            vm.TotalAMKindergarten = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.Kindergarten).Count();
            vm.TotalAM1st = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.First).Count();
            vm.TotalAM2nd = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.Second).Count();
            vm.TotalAM3rd = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.Third).Count();
            vm.TotalAM4th = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.Fourth).Count();
            vm.TotalAM5th = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.Fifth).Count();
            vm.TotalAM6th = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.AM && c.GradeCompleted == Enums.ClassGrade.Sixth).Count();
            vm.TotalPMPreschool = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.PreSchool).Count();
            vm.TotalPMPreK = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.PreK).Count();
            vm.TotalPMKindergarten = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.Kindergarten).Count();
            vm.TotalPM1st = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.First).Count();
            vm.TotalPM2nd = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.Second).Count();
            vm.TotalPM3rd = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.Third).Count();
            vm.TotalPM4th = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.Fourth).Count();
            vm.TotalPM5th = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.Fifth).Count();
            vm.TotalPM6th = vBS.Children.Where(c => c.Session.Period == Enums.SessionPeriod.PM && c.GradeCompleted == Enums.ClassGrade.Sixth).Count();
            vm.TotalAttendNWB = vBS.Children.Where(c => c.AttendHostChurch == true).Count();
            vm.TotalVisitors = vBS.Children.Where(c => c.AttendHostChurch == false).Count();

            return View(vm);
        }

        // GET: VBS/Current/5
        [Authorize(Policy = "TenantAdmin")]
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
        [Authorize(Policy = "TenantAdmin")]
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
        [Authorize(Policy = "TenantAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VBS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> Create([Bind("EndDate,StartDate,ThemeName,AMStartTime,AMEndTime,AMMaxChildren,PMStartTime,PMEndTime,PMMaxChildren,FormStackAPIKey,FormStackFormId,FormStackImportPageKey")] CreateViewModel vBSCreateViewModel)
        {


            var vBS = new VBS {
                ThemeName = vBSCreateViewModel.ThemeName,
                EndDate = vBSCreateViewModel.EndDate,
                StartDate = vBSCreateViewModel.StartDate,
                TenantId = this.TenantId,
                FormStackAPIKey = vBSCreateViewModel.FormStackAPIKey,
                FormStackFormId = vBSCreateViewModel.FormStackFormId,
                FormStackImportPageKey = vBSCreateViewModel.FormStackImportPageKey
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
            return View(vBSCreateViewModel);
        }

        // GET: VBS/Edit/5
        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> Edit(int? id)
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
            
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "ChurchName", vBS.TenantId);
            return View(vBS);
        }

        // POST: VBS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EndDate,StartDate,ThemeName,FormStackAPIKey,FormStackFormId,FormStackImportPageKey")] VBS vBS)
        {
            if (id != vBS.Id)
            {
                return NotFound();
            }

            var vbsContext = _context.VBS.Where(v => v.Id == vBS.Id && v.TenantId == this.TenantId).FirstOrDefault();

            //Set the tenant id based on the current user context.
            vBS.TenantId = this.TenantId;

            vbsContext.TenantId = this.TenantId;
            vbsContext.EndDate = vBS.EndDate;
            vbsContext.StartDate = vBS.StartDate;
            vbsContext.ThemeName = vBS.ThemeName;
            vbsContext.FormStackAPIKey = vBS.FormStackAPIKey;
            vbsContext.FormStackFormId = vBS.FormStackFormId;
            vbsContext.FormStackImportPageKey = vBS.FormStackImportPageKey;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vbsContext);
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
        [Authorize(Policy = "TenantAdmin")]
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
        [Authorize(Policy = "TenantAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var vBS = await _context.VBS.Include(v => v.Tenant).Where(vb => vb.Id == id && vb.TenantId == this.TenantId).SingleOrDefaultAsync();
            //var vBS = await _context.VBS.SingleOrDefaultAsync(m => m.Id == id);
            _context.VBS.Remove(vBS);
            await _context.SaveChangesAsync();

            //TODO: Need to reset the cookie if the current VBS was deleted.

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExportClassAttendanceToExcel()
        {
            var applicationDbContext = _context.Classes.Include(c => c.Session)
                .Include(c => c.Children)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name);

            List<Classroom> dbClassrooms = await applicationDbContext.ToListAsync();

            string tenantName = _context.Tenants
                .Where(t => t.Id == this.TenantId).First().Name;

            string theme = _context.VBS
                .Where(v => v.Id == this.CurrentVBSId).First().ThemeName;

            string fileName = tenantName + " - " + theme + " - " + "Attendance.xlsx";

            byte[] filecontent = ExcelExportHelper.ExportAttendanceExcel(dbClassrooms);
            return File(filecontent, ExcelExportHelper.ExcelContentType, fileName);
        }

        [HttpGet]
        public async Task<IActionResult> ExportVBSToExcel()
        {
            var applicationDbContext = _context.Classes.Include(c => c.Session)
                .Include(c => c.Children)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.Grade)
                .ThenBy(c => c.Name);

            List<Classroom> dbClassrooms = await applicationDbContext.ToListAsync();

            var applicationDbContext2 = _context.Children.Include(c => c.Classroom)
                .Include(c => c.Classroom.Session)
                .Include(c => c.Session)
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId)
                .OrderBy(c => c.SessionId)
                .ThenBy(c => c.LastName)
                .ThenBy(c => c.FirstName);

            List<Child> dbChildren = await applicationDbContext2.ToListAsync();



            string tenantName = _context.Tenants
                .Where(t => t.Id == this.TenantId).First().Name;

            string theme = _context.VBS
                .Where(v => v.Id == this.CurrentVBSId).First().ThemeName;

            string fileName = tenantName + " - " + theme + " - " + "Full Data Export.xlsx";

            byte[] filecontent = ExcelExportHelper.ExportVBSExcel(dbClassrooms, dbChildren);
            return File(filecontent, ExcelExportHelper.ExcelContentType, fileName);
        }


        [HttpGet]
        public async Task<IActionResult> ExportUnchurchedToExcel()
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

                        vm.ChildrenNamesAndGrades += ", " + dbChild.FirstName + " " + dbChild.LastName + "(" + dbChild.GradeCompleted + ")";
                    }
                }
            }

            foreach (UnchurchedReportViewModel2 vm2 in vmDictionary.Values)
            {
                vms.Add(vm2);
            }

            string tenantName = _context.Tenants
                .Where(t => t.Id == this.TenantId).First().Name;

            string theme = _context.VBS
                .Where(v => v.Id == this.CurrentVBSId).First().ThemeName;

            string fileName = tenantName + " - " + theme + " - " + "Unchurched Children Export.xlsx";

            byte[] filecontent = ExcelExportHelper.ExportUnchurchedExcel(vms);
            return File(filecontent, ExcelExportHelper.ExcelContentType, fileName);
        }


        [HttpGet]
        [Route("ClassAssignmentEmailExport.csv")]
        [Produces("text/csv")]
        public async Task<IActionResult> ExportClassAssignmentEmailDataAsCsv()
        {
            var models = new List<ClassAssignmentEmailModel>();

            List<Child> uniqueGuardians = await _context.Children
                .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId && c.ClassroomId != null && !String.IsNullOrEmpty(c.GuardianEmail))
                .OrderBy(c => c.GuardianLastName)
                .GroupBy(c => c.GuardianEmail)
                .Select(c => c.First())
                .ToListAsync();

            foreach(Child guardian in uniqueGuardians)
            {
                List<Child> children = await _context.Children
                    .Include(c => c.Classroom)
                    .Include(c => c.Classroom.Session)
                    .Where(c => c.VBSId == this.CurrentVBSId && c.VBS.TenantId == this.TenantId && !String.IsNullOrEmpty(c.GuardianEmail) && c.ClassroomId != null && c.GuardianEmail == guardian.GuardianEmail)
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName).ToListAsync();

                int childcount = children.Count;

                ClassAssignmentEmailModel model = new ClassAssignmentEmailModel();
                model.GuardianName = guardian.GuardianFirstName + " " + guardian.GuardianLastName;
                model.Email = guardian.GuardianEmail;
     
                var childNames = new string[10];
                var childAssignments = new string[10];
                var childPasswords = new string[10];

                for (int i = 0; i < 10; i++)
                {
                    if (i < childcount)
                    {
                        var child = children[i];
                        childNames[i] = child.FirstName + " " + child.LastName;
                        childAssignments[i] = child.Classroom.Session.Period + " " + child.Classroom.Grade.GetDisplayName() + " " + child.Classroom.Name;
                        childPasswords[i] = child.Password ?? String.Empty;
                    }
                    else
                    {
                        childNames[i] = String.Empty;
                        childAssignments[i] = String.Empty;
                        childPasswords[i] = String.Empty;
                    }
                }

                model.ChildName1 = childNames[0];
                model.ChildPassword1 = childPasswords[0];
                model.AssignedClassName1 = childAssignments[0];
                model.ChildName2 = childNames[1];
                model.ChildPassword2 = childPasswords[1];
                model.AssignedClassName2 = childAssignments[1];
                model.ChildName3 = childNames[2];
                model.ChildPassword3 = childPasswords[2];
                model.AssignedClassName3 = childAssignments[2];
                model.ChildName4 = childNames[3];
                model.ChildPassword4 = childPasswords[3];
                model.AssignedClassName4 = childAssignments[3];
                model.ChildName5 = childNames[4];
                model.ChildPassword5 = childPasswords[4];
                model.AssignedClassName5 = childAssignments[4];
                model.ChildName6 = childNames[5];
                model.ChildPassword6 = childPasswords[5];
                model.AssignedClassName6 = childAssignments[5];
                model.ChildName7 = childNames[6];
                model.ChildPassword7 = childPasswords[6];
                model.AssignedClassName7 = childAssignments[6];
                model.ChildName8 = childNames[7];
                model.ChildPassword8 = childPasswords[7];
                model.AssignedClassName8 = childAssignments[7];
                model.ChildName9 = childNames[8];
                model.ChildPassword9 = childPasswords[8];
                model.AssignedClassName9 = childAssignments[8];
                model.ChildName10 = childNames[9];
                model.ChildPassword10 = childPasswords[9];
                model.AssignedClassName10 = childAssignments[9];

                models.Add(model);
            }

            return Ok(models);
        }


        // GET: VBS/Import/5/abcdefg
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Import(int? id, string pageImportKey)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Need to verify the requested VBS exists
            var vBS = await _context.VBS.Include(s => s.Sessions).Where(vb => vb.Id == id).SingleOrDefaultAsync();
            if (vBS == null)
            {
                return NotFound();
            }

            //Verify the supplied page import key matches the value saved with the VBS in the database.
            if (vBS.FormStackImportPageKey != pageImportKey)
            {
                return NotFound();
            }

            //Don't go further if the formstack fields aren't populated in the database
            if(string.IsNullOrWhiteSpace(vBS.FormStackAPIKey) || vBS.FormStackFormId == null)
            {
                return NotFound();
            }


            string baseAddress = "https://www.formstack.com/api/v1/";
            string apiKey = vBS.FormStackAPIKey;
            string responseType = "json";

            int amSessionId, pmSessionId;
            if(vBS.Sessions[0].Period == Enums.SessionPeriod.AM)
            {
                amSessionId = vBS.Sessions[0].Id;
                pmSessionId = vBS.Sessions[1].Id;
            }
            else
            {
                amSessionId = vBS.Sessions[1].Id;
                pmSessionId = vBS.Sessions[0].Id;
            }

            FormStackHelper fsHelper = new FormStackHelper(baseAddress, apiKey, responseType, vBS, amSessionId, pmSessionId, _context);
            await fsHelper.PopulateLatestSubmissions(vBS.FormStackFormId.ToString(), vBS.FormStackLastImportDateTime.ToString());

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ImportV2(int? id, string pageImportKey)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Need to verify the requested VBS exists
            var vBS = await _context.VBS.Include(s => s.Sessions).Where(vb => vb.Id == id).SingleOrDefaultAsync();
            if (vBS == null)
            {
                return NotFound();
            }

            //Verify the supplied page import key matches the value saved with the VBS in the database.
            if (vBS.FormStackImportPageKey != pageImportKey)
            {
                return NotFound();
            }

            //Don't go further if the formstack fields aren't populated in the database
            if (string.IsNullOrWhiteSpace(vBS.FormStackAPIKey) || vBS.FormStackFormId == null)
            {
                return NotFound();
            }


            string baseAddress = "https://www.formstack.com/api/v2/";

            int amSessionId, pmSessionId;
            if (vBS.Sessions[0].Period == Enums.SessionPeriod.AM)
            {
                amSessionId = vBS.Sessions[0].Id;
                pmSessionId = vBS.Sessions[1].Id;
            }
            else
            {
                amSessionId = vBS.Sessions[1].Id;
                pmSessionId = vBS.Sessions[0].Id;
            }

            FormStackHelper fsHelper = new FormStackHelper(baseAddress, vBS.FormStackAPIKey, vBS, amSessionId, pmSessionId, _context);
            await fsHelper.PopulateLatestSubmissionsV2(vBS.FormStackFormId.ToString(), vBS.FormStackLastImportDateTime.ToString());

            return Ok();
        }


        private bool VBSExists(int id)
        {
            return _context.VBS.Any(e => e.Id == id);
        }
    }
}
