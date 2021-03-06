﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Data.VBSAdminModels
{
    public class Child
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
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [Required]
        public Enums.ClassGrade GradeCompleted { get; set; }
        [Column(TypeName = "Date")]
        [Required]
        [DataType(DataType.Date)]
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
        public string HomeChurch { get; set; }
        public bool AttendHostChurch { get; set; }
        public string InvitedBy { get; set; }
        public DateTime DateOfRegistration { get; set; }


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

        public string EmergencyContactChildRelationship { get; set; }


        //Classroom Assignment
        public int? ClassroomId { get; set; }
        public Classroom Classroom { get; set; }


        //VBS & Session
        [Required]
        public int SessionId { get; set; }
        public Session Session { get; set; }

        [Required]
        public int VBSId { get; set; }
        public VBS VBS { get; set; }
    }
}
