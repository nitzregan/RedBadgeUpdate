using RedBadge.Data;
using RedBadge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Services
{
    public class EventService
    {
        private readonly Guid _userId;

        public EventService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateEvent(EventCreate model)
        {
            var entity =
                new Event()
                {
                    UserID = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    AllDayEvent = model.AllDayEvent,
                    Start = model.Start,
                    End = model.End
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Event.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public EventDetail GetEventById(int EventID, int TeamID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Event
                        .Where(e => e.TeamID == TeamID)
                        .Single(e => e.EventID == EventID);

                return
                new EventDetail
                {
                    EventID = entity.EventID,
                    Title = entity.Title,
                    Content = entity.Content,
                    AllDayEvent = entity.AllDayEvent,
                    Start = entity.Start,
                    End = entity.End
                };
            }
        }

        public bool UpdateEvent(EventEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Event
                        .Where(e => e.UserID == _userId)
                        .Single(e => e.EventID == model.EventID);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.AllDayEvent = model.AllDayEvent;
                entity.Start = model.Start;
                entity.End = model.End;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteEvent(int EventID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Event
                        .Where(e => e.UserID == _userId)
                        .Single(e => e.EventID == EventID);

                ctx.Event.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<EventListItem> GetEventsByTeam(int ID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Event
                    .Where(e => e.TeamID == ID) ///figure this out tomorrow
                    .Select(
                        e =>
                            new EventListItem
                            {
                                EventID = e.EventID,
                                Title = e.Title,
                                Content = e.Content,
                                AllDayEvent = e.AllDayEvent,
                                Start = e.Start,
                                End = e.End
                            }
                        );
                return query.ToArray();
            }
        }
    }
}
