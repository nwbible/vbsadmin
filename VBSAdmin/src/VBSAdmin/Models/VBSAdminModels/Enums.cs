using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSAdminModels
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
            [Display(Name = "PreSchool")]
            PreSchool=0,
            [Display(Name = "PreK")]
            PreK=1,
            [Display(Name = "Kindergarten")]
            Kindergarten=2,
            First=3,
            Second=4,
            Third=5,
            Fourth=6,
            Fifth=7,
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
    }
}
