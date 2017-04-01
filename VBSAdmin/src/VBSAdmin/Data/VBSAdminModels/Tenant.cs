using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace VBSAdmin.Data.VBSAdminModels
{
    public class Tenant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Church Name")]
        public string ChurchName { get; set; }
        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string ContactPhone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string ContactEmail { get; set; }

        public List<VBS> VBSList { get; set; }
    }
}
