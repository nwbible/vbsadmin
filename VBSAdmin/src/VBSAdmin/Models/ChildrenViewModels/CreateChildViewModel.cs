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
    public class CreateChildViewModel
    {
        //Child
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public Enums.ClassGrade GradeCompleted { get; set; }
        [Column(TypeName = "Date")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Enums.ChildGender Gender { get; set; }
        public string AllergiesDescription { get; set; }
        public string MedicalConditionDescription { get; set; }
        public string MedicationDescription { get; set; }
        public string PlaceChildWithRequest { get; set; }
        public bool DecisionMade { get; set; }
        public string HomeChurch { get; set; }
        public bool AttendHostChurch { get; set; }
        public string InvitedBy { get; set; }


        //Guardian & Emergency Contact (They may be different)
        [Required]
        public string GuardianFirstName { get; set; }
        [Required]
        public string GuardianLastName { get; set; }
        [Required]
        [Phone]
        public string GuardianPhone { get; set; }
        [Required]
        [EmailAddress]
        public string GuardianEmail { get; set; }
        public string GuardianChildRelationship { get; set; }
        [Required]
        public string EmergencyContactFirstName { get; set; } 
        [Required]
        public string EmergencyContactLastName { get; set; } 
        [Required]
        [Phone]
        public string EmergencyContactPhone { get; set; }
        public int? ClassroomId { get; set; }


        public List<SelectListItem> ClassroomSelectItems { get; set; }


        //VBS & Session
        [Required]
        public int SessionId { get; set; }

    }
}
