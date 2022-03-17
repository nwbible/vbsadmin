using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Data.VBSAdminModels
{
    public class Enums
    {
        public enum SessionPeriod
        {
            [Display(Name = "AM")]
            AM = 0,
            [Display(Name = "PM")]
            PM = 1
        }

        public enum ClassGrade
        {
            [Display(Name = "4 Yrs")]
            PreSchool=0,
            [Display(Name = "5 Yrs")]
            PreK=1,
            [Display(Name = "Kindergarten")]
            Kindergarten=2,
            [Display(Name = "First Grade")]
            First=3,
            [Display(Name = "Second Grade")]
            Second=4,
            [Display(Name = "Third Grade")]
            Third=5,
            [Display(Name = "Fourth Grade")]
            Fourth=6,
            [Display(Name = "Fifth Grade")]
            Fifth=7,
            [Display(Name = "Sixth Grade")]
            Sixth=8
        }
        
        public enum ClassGender
        {
            [Display(Name = "Boy")]
            Boy=0,
            [Display(Name = "Girl")]
            Girl =1,
            [Display(Name = "Mixed")]
            Mixed=2
        }

        public enum ChildGender
        {
            [Display(Name = "Boy")]
            Boy=0,
            [Display(Name = "Girl")]
            Girl=1
        }

        public enum AssignmentOptions
        {
            Assigned=0,
            Unassigned=1,
            All=2
        }

        public enum ClassGradeOptions
        {
            [Display(Name = "4 Yrs")]
            PreSchool = 0,
            [Display(Name = "PreK")]
            PreK = 1,
            [Display(Name = "K")]
            Kindergarten = 2,
            [Display(Name = "1st")]
            First = 3,
            [Display(Name = "2nd")]
            Second = 4,
            [Display(Name = "3rd")]
            Third = 5,
            [Display(Name = "4th")]
            Fourth = 6,
            [Display(Name = "5th")]
            Fifth = 7,
            [Display(Name = "6th")]
            Sixth = 8,
            All = 9
        }
    }
}
