using RedBadge.Data;
using RedBadge.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Services
{
    public class TeamMessagingService
    {
        private readonly Guid _userID;

        public TeamMessagingService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateTeamMessaging(TeamMessagingCreate model)
        {
            byte[] bytes = null;
            if (model.File != null){ 

            Stream fs = model.File.InputStream;
            BinaryReader br = new BinaryReader(fs);
            bytes = br.ReadBytes((Int32)fs.Length);

            }

            var entity =
                new TeamMessaging()
                {
                    FileContent = bytes,
                    UserID = _userID,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTime.Now,
                    TeamID = model.TeamID,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.TeamMessaging.Add(entity); 
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamMessagingListItem> GetTeamMessages()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .TeamMessaging
                    .Where(e => e.Roster.SingleOrDefault(i => i.UserID == _userID) != null)
                    .Select(
                        e =>
                            new TeamMessagingListItem
                            {
                                FileContent = e.FileContent,
                                MessageID = e.MessageID,
                                Title = e.Title,
                                Content = e.Content,
                                CreatedUtc = e.CreatedUtc,
                                Modifiedutc = e.Modifiedutc,
                            }
                        );

                return query.ToArray();
            }
        }

        public TeamMessagingDetail GetTeamMessageById(int MessageID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TeamMessaging
                        .Single(e => e.MessageID == MessageID);
                if (entity.Roster.SingleOrDefault(e => e.UserID == _userID) != null)
                {
                    return
                    new TeamMessagingDetail
                    {
                        FileContent = entity.FileContent,
                        MessageID = entity.MessageID,
                        Title = entity.Title,
                        Content = entity.Content,
                    };
                }
                else
                {
                    return null;
                }

            }
        }

        public bool UpdateTeamMessage(TeamMessagingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                byte[] bytes = null;
                if (model.File != null)
                {

                    Stream fs = model.File.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);

                }

                var entity =
                    ctx
                        .TeamMessaging
                        .Single(e => e.MessageID == model.MessageID && e.UserID == _userID);

                entity.FileContent = bytes;
                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.Modifiedutc = DateTime.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeamMessage(int MessageID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TeamMessaging
                        .Single(e => e.MessageID == MessageID && e.UserID == _userID);

                ctx.TeamMessaging.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
