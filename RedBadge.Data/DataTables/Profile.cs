using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Data
{
    public class Profile
    {
        [Required]
        public Guid UserID { get; set; }
        [Key]
        public int ProfileID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Late Name")]
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string OtherInfo { get; set; }
        public ICollection<Team> MyTeams { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset? CreatedUtc { get; set; }
    }
}
