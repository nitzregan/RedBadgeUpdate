using RedBadge.Data;
using RedBadge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBadge.Services
{
    public class TeamService
    {
        private readonly Guid _userID;

        public TeamService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateTeam(TeamCreate model, int ProfileID)
        {
           
            var entity =
                new Team()
                {
                    UserID = model.UserID,
                    TeamName = model.TeamName,
                    Roster = new List<Profile>(),
                    TeamEvents = new List<Event>()
                };
            using (var ctx = new ApplicationDbContext())
            {
                

                ctx.Team.Add(entity);
                
                if (ctx.SaveChanges() == 1)
                {
                    var query =
                   ctx
                       .Profile
                       .Where(e => e.ProfileID == ProfileID)
                       .Single();
                    entity.Roster.Add(query);
                }
                
                return ctx.SaveChanges() == 1;
            }
        } 

        public IEnumerable<TeamListItem> GetAllTeamsForCoachByUserID(Guid UserID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Team
                    .Where(e => e.UserID == _userID)
                    .Select(
                        e =>
                            new TeamListItem
                            {
                                UserID = _userID,
                                TeamID = e.TeamID,
                                TeamName = e.TeamName,
                                Roster = e.Roster,
                                TeamEvents = e.TeamEvents
                            }
                        );
                return query.ToArray();
            }
        }


        public TeamDetail GetTeamById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Team
                    .Single(e => e.TeamID == id);
                if (entity.Roster.SingleOrDefault(e => e.UserID == entity.UserID) != null)
                {

                    return
                        new TeamDetail
                        {
                            TeamID = entity.TeamID,
                            TeamName = entity.TeamName,
                            Roster = entity.Roster,
                            TeamEvents = entity.TeamEvents
                        };
                }
                else
                {
                    return null;
                };
            }
        }

        public bool AddAthleteToRosterByProfileID(int ProfileID, int TeamID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Profile
                        .Where(e => e.ProfileID == ProfileID)
                        .Single();
                var queryTwo =
                   ctx
                        .Team
                        .Include("Roster")
                        .Single(e => e.TeamID == TeamID);
                queryTwo.Roster.Add(query);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool RemoveAthleteFromRosterByProfileID(int ProfileID, int TeamID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Team
                        .Include("Roster")
                        .Single(e => e.TeamID == TeamID);
                var queryTwo =
                   ctx
                        .Profile
                        .Where(e => e.ProfileID == ProfileID)
                        .Single();
                query.Roster.Remove(queryTwo);
                return ctx.SaveChanges() == 1;
            }
        }

        //public bool RemoveAthleteToRosterByProfileID(int ProfileID, int TeamID)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .Profile
        //                .Where(e => e.ProfileID == ProfileID)
        //                .Single();
        //        var queryTwo =
        //           ctx
        //                .Team
        //                .Include("Roster")
        //                .Single(e => e.TeamID == TeamID);
        //        queryTwo.Roster.Remove(query);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        //add "duplicate athlete" protection, ask andrew
        //if (!newTeam.Golfers.Contains(newGolfer.Golfer))
        //            {
        //                if (SumGolferRanking(teamId) + newGolfer.PowerRanking <= 13)
        //                {
        //                    newTeam.Golfers.Add(newGolfer.Golfer);
        //                    ctx.SaveChanges();
        //                    return "Done";
        //                }
        //                else
        //                {
        //                    return "Power Ranking";
        //                }
        //            }
        //            else
        //            {
        //                return "Duplicate Golfer";
        //            }


        //public ICollection<Profile> RemoveAthleteFromRosterByProfileID(int ProfileID, int TeamID)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .Team
        //                .Where(e => e.TeamID == TeamID)
        //                .Single().Roster;

        //        var queryTwo =
        //           ctx
        //                .Profile
        //                .Where(e => e.ProfileID == ProfileID)
        //                .Single();

        //        query.Remove(queryTwo);
        //        ctx.SaveChanges();
        //        return query.ToList();
        //    }
        //}

        public IEnumerable<TeamListItem> GetAllTeamsForAthleteByUserID(Guid UserID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Profile
                    .Where(e => e.UserID == _userID)
                    .Single().MyTeams
                    .Select(
                        e =>
                            new TeamListItem
                            {
                                UserID = _userID,
                                TeamID = e.TeamID,
                                TeamName = e.TeamName,
                                Roster = e.Roster,
                                TeamEvents = e.TeamEvents
                            }
                        );
                return entity.ToArray();
            }
        }

        public bool UpdateTeam(TeamEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Team
                        //.Include("Roster")
                        //.Include("TeamEvents")
                        .Single(e => e.TeamID == model.TeamID && e.UserID == model.UserID);

                query.TeamName = model.TeamName;
                //entity.Roster = model.Roster;
                //entity.TeamEvents = model.TeamEvents;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeam(int TeamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Team
                        .Single(e => e.TeamID == TeamId && e.UserID == _userID);

                ctx.Team.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
