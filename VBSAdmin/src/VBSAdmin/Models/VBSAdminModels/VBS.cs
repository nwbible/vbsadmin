using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSAdminModels
{
    public class VBS
    {
        public int Id { get; set; }
        public Tenant Tenant { get; set; }
        public int TenantId { get; set; }
        [Required]
        public string ThemeName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public List<Session> Sessions { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; set; }


        //TODO:
        //Theme image
    }
}
