using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSViewModels
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Theme")]
        public string ThemeName { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Form Stack API Key")]
        public string FormStackAPIKey { get; set; }
        [Display(Name = "Form Stack Form Id")]
        public int? FormStackFormId { get; set; }
        [Display(Name = "Form Stack Import Page Key")]
        public string FormStackImportPageKey { get; set; }

    }
}
