using RedBadge.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Model
{
    public class TeamCreate
    {
        [Required]
        public Guid UserID { get; set; }
        [Key]
        public int TeamID { get; set; }
        [Required]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }
        public ICollection<Profile> Roster { get; set; }
        public ICollection<Event> TeamEvents { get; set; }
    }
}
