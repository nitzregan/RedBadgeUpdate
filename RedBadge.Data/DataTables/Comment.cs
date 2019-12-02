using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Data
{
    public class Comment
    {
        [Required]
        public Guid UserID { get; set; }
        [ForeignKey("ProfileVariable")]
        public int? ProfileID { get; set; }

        public string Name { get; set; }
        public virtual Profile ProfileVariable { get; set; }
        public int CommentID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Sent")]
        public DateTimeOffset DateSent { get; set; }
        
    }
}
