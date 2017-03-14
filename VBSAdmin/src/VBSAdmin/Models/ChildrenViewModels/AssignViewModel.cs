using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using VBSAdmin.Data.VBSAdminModels;

namespace VBSAdmin.Models.ChildrenViewModels
{
    public class AssignViewModel
    {
        [Display(Name = "Grade")]
        public Enums.ClassGradeOptions FilterGrade { get; set; }
        [Display(Name = "Name")]
        public string FilterName { get; set; }
        [Display(Name = "Assignment")]
        public Enums.AssignmentOptions AssignmentOption { get; set; }
        [Display(Name = "Session")]
        public Enums.SessionPeriod SessionOption { get; set; }
        public List<SelectListItem> AMClassroomSelectItems { get; set; }
        public List<SelectListItem> PMClassroomSelectItems { get; set; }
        public Classroom GroupClassAssignment { get; set; }
        public List<AssignChild> Children { get; set; }
        public List<ClassAssignment> Assignments { get; set; }
    }

    public class AssignChild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GuardianName { get; set; }
        public Enums.ClassGrade GradeCompleted { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceChildWithRequest { get; set; }
        public string Allergies { get; set; }
        public string MedicalConditions { get; set; }
        public string CurrentClassName { get; set; }
        public Session Session { get; set; }
        public int CurrentClassId { get; set; }
        public int NewClassId { get; set; }
    }

    public class ClassAssignment
    {
        public int? CurrentClassId { get; set; } 
        public int? NewClassId { get; set; }
        public int ChildId { get; set; }
    }
}
