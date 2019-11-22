using RedBadge.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Model
{
    public class ProfileListItem
    {
        [Required]
        public Guid UserID { get; set; }
        [Key]
        public int ProfileID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Late Name")]
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string OtherInfo { get; set; }
        public string AthleteUsername { get; set; }
        public string ParentUsername { get; set; }
        public ICollection<Team> MyTeams { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public DateTimeOffset? CreatedUtc { get; set; }
    }
}
