using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VBSAdmin.Data.VBSAdminModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VBSAdmin.Models.ChildrenViewModels
{
    public class DetailsChildViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Child Name")]
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [Display(Name = "Grade Completed")]
        public Enums.ClassGrade GradeCompleted { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public Enums.ChildGender Gender { get; set; }
        [Display(Name = "Allergies")]
        public string AllergiesDescription { get; set; }
        [Display(Name = "Medical Conditions")]
        public string MedicalConditionDescription { get; set; }
        [Display(Name = "Medications")]
        public string MedicationDescription { get; set; }
        [Display(Name = "Place Child With Request")]
        public string PlaceChildWithRequest { get; set; }
        [Display(Name = "Decision Made at VBS")]
        public bool DecisionMade { get; set; }
        [Display(Name = "Home Church")]
        public string HomeChurch { get; set; }
        [Display(Name = "Attends NWB")]
        public bool AttendHostChurch { get; set; }
        [Display(Name = "Invited By")]
        public string InvitedBy { get; set; }


        //Guardian & Emergency Contact (They may be different)
        [Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }
        [Display(Name = "Guardian Phone")]
        public string GuardianPhone { get; set; }
        [Display(Name = "Guardian Email")]
        public string GuardianEmail { get; set; }
        [Display(Name = "Guardian Relationship")]
        public string GuardianChildRelationship { get; set; }
        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContactName { get; set; }
        [Display(Name = "Emergency Contact Phone")]
        public string EmergencyContactPhone { get; set; }
        [Display(Name = "Emergency Contact Relationship")]
        public string EmergencyContactChildRelationship { get; set; }
        [Display(Name = "Assigned Classroom")]
        public string ClassroomName { get; set; }

        [Display(Name = "Session")]
        public Enums.SessionPeriod Period { get; set; }
    }
}
