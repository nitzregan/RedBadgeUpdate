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
    public class TeamMessagingListItem
    {
        [Required]
        public Guid UserID { get; set; }
        [Key]
        public int MessageID { get; set; }
        [ForeignKey("TeamVariable")]
        public int TeamID { get; set; }
        public virtual Team TeamVariable { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public DateTimeOffset? CreatedUtc { get; set; }
        public DateTimeOffset? Modifiedutc { get; set; }
        public ICollection<Profile> Roster { get; set; }
    }
}
