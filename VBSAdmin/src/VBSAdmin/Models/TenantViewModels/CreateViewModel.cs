﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.TenantViewModels
{
    public class CreateViewModel
    {

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

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
