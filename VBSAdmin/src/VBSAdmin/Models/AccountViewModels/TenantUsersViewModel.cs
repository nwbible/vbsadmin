using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
    

namespace VBSAdmin.Models.AccountViewModels
{
    public class UserListViewModel
    {
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Display(Name = "Tenant Admin")]
        public string IsTenantAdmin { get; set; }

        public int TenantId { get; set; }
    }
}

