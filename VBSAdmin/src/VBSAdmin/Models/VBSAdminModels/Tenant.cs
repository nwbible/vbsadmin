using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace VBSAdmin.Models.VBSAdminModels
{
    public class Tenant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string ChurchName { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        [Phone]
        public string ContactPhone { get; set; }
        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        public List<VBS> VBSList { get; set; }
    }
}
