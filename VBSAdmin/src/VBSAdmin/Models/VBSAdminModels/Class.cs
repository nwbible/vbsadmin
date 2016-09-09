using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
    

namespace VBSAdmin.Models.VBSAdminModels
{
    public class Class
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Enums.ClassGrade Grade { get; set; }

        [Required]
        public Enums.ClassGender Gender { get; set; }

        [Required]
        public int VBSId { get; set; }

        public int SessionId { get; set; }

        public List<Child> Children { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
