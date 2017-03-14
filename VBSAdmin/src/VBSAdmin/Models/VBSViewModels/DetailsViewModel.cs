using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSViewModels
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Theme")]
        public string ThemeName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public int TotalRegisteredChildren { get; set; }
        public int TotalAMRegisteredChildren { get; set; }
        public int TotalPMRegisteredChildren { get; set; }


        public int TotalAMPreschool { get; set; }
        public int TotalAMPreK { get; set; }
        public int TotalAMKindergarten { get; set; }
        public int TotalAM1st { get; set; }
        public int TotalAM2nd { get; set; }
        public int TotalAM3rd { get; set; }
        public int TotalAM4th { get; set; }
        public int TotalAM5th { get; set; }
        public int TotalAM6th { get; set; }

        public int TotalPMPreschool { get; set; }
        public int TotalPMPreK { get; set; }
        public int TotalPMKindergarten { get; set; }
        public int TotalPM1st { get; set; }
        public int TotalPM2nd { get; set; }
        public int TotalPM3rd { get; set; }
        public int TotalPM4th { get; set; }
        public int TotalPM5th { get; set; }
        public int TotalPM6th { get; set; }

        public int TotalAttendNWB { get; set; }
        public int TotalVisitors { get; set; }

    }
}
