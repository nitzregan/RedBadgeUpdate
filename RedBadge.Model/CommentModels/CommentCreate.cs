using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Model
{
    public class CommentCreate
    {
        public Guid UserID { get; set; }
        public int ProfileID { get; set; }
        public int CommentID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Sent")]
        public DateTimeOffset DateSent { get; set; }

    }
}
