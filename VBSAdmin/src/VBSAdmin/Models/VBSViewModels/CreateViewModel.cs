using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSViewModels
{
    public class CreateViewModel
    {
        
        [Required]
        public string ThemeName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime AMStartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime AMEndTime { get; set; }

        [Required]
        public int AMMaxChildren { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime PMStartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime PMEndTime { get; set; }

        [Required]
        public int PMMaxChildren { get; set; }
        public string FormStackAPIKey { get; set; }
        public int? FormStackFormId { get; set; }
        public string FormStackImportPageKey { get; set; }
    }
}
