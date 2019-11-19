using RedBadge.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Model
{
    public class EventCreate
    {
        [Required]
        public Guid UserID { get; set; }
        [Key]
        public int EventID { get; set; }
        [ForeignKey("TeamVariable")]
        public int TeamID { get; set; }
        public virtual Team TeamVariable { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool AllDayEvent { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
    }
}
