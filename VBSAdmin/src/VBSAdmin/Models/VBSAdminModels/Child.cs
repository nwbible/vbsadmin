using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSAdminModels
{
    public class Child
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Enums.ClassGrade GradeCompleted { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Enums.ChildGender Gender { get; set; }
        [Required]
        public bool HasAllergies { get; set; }
        public string AllergiesDescription { get; set; }
        [Required]
        public bool HasMedicalCondition { get; set; }
        public string MedicalConditionDescription { get; set; }
        [Required]
        public bool HasMedications { get; set; }
        public string MedicationDescription { get; set; }
        public string PlaceChildWithRequest { get; set; }
        public bool DecisionMade { get; set; }

        [Required]
        public int GuardianId { get; set; }
        public Guardian Guardian { get; set; }

        public int ClassId { get; set; }
        public VBSAdminModels.Class Class { get; set; }

        [Required]
        public int SessionId { get; set; }
        public Session Session { get; set; }

        [Required]
        public int VBSId { get; set; }
        public VBS VBS { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
