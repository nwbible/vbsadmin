using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSAdminModels
{
    public class Guardian
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        [Phone]
        public string PrimaryPhone { get; set; }
        [Phone]
        public string SecondaryPhone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string EmergencyContact { get; set; } //TODO: What exactly is this?
        [Required]
        public string ChildRelationship { get; set; }
        public string HomeChurch { get; set; }
        public string AttendHostChurch { get; set; }
        public string InvitedBy { get; set; }

        public int VBSId { get; set; }
        public int SessionId { get; set; }

        public List<Child> Children { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}
