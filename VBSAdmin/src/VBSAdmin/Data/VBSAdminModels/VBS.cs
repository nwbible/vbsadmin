﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Data.VBSAdminModels
{
    public class VBS
    {
        public int Id { get; set; }
        public Tenant Tenant { get; set; }
        [Required]
        public int TenantId { get; set; }
        [Required]
        [Display(Name = "Theme")]
        public string ThemeName { get; set; }
        [Column(TypeName = "Date")]
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "Date")]
        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public List<Session> Sessions { get; set; }

        public List<Child> Children { get; set; }

        public List<Classroom> Classrooms { get; set; }

        public string FormStackImportPageKey { get; set; }
        public int? FormStackFormId { get; set; }
        public string FormStackAPIKey { get; set; }
        public DateTime FormStackLastImportDateTime { get; set; }

        //TODO:
        //Theme image
    }
}
