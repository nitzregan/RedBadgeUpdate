using RedBadge.Data;
using RedBadge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RedBadge.Services
{
    public class CommentService
    {
        private readonly Guid _userId;
        public CommentService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateComment(CommentCreate model, int ProfileID)
        {
            var entity =
                new Comment()
                {
                    UserID = _userId,
                    ProfileID = model.ProfileID,
                    Title = model.Title,
                    Content = model.Content,
                    Name = model.Name,
                    DateSent = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                .Profile
                .Include("Comments")
                .Where(e => e.ProfileID == ProfileID)
                .Single();
                query.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public CommentDetail GetCommentById(int CommentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                   ctx
                       .Comment
                       //.Where(e => e.ProfileID == ProfileID)
                       .Single(e => e.CommentID == CommentID);
                return
                new CommentDetail
                {
                    UserID = entity.UserID,
                    ProfileID = entity.ProfileID,
                    CommentID = entity.CommentID,
                    Title = entity.Title,
                    Content = entity.Content,
                    Name = entity.Name,
                    DateSent = entity.DateSent
                };
            }
        }
        public bool DeleteComment(int CommentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comment
                        .Where(e => e.UserID == _userId)
                        .Single(e => e.CommentID == CommentID);
                //var entity1 =
                //    ctx
                //        .Profile
                //        .Include("Comments")
                //        .Single(e => e.ProfileID == ProfileID);
                ctx.Comment.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CommentListItem> GetCommentsByProfile(int ProfileID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Comment
                    .Where(e => e.ProfileID == ProfileID)
                    .Select(
                        e =>
                            new CommentListItem
                            {
                                UserID = _userId,
                                ProfileID = e.ProfileID,
                                Title = e.Title,
                                CommentID = e.CommentID,
                                Content = e.Content,
                                Name = e.Name,
                                DateSent = e.DateSent
                            }
                        );
                return query.ToArray();
            }
        }
    }
}


