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

        public bool CreateTeam(TeamCreate model)
        {
            var entity =
                new Team()
                {
                    TeamName = model.TeamName,
                    Roster = model.Roster,
                    TeamEvents = model.TeamEvents
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Team.Add(entity);
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
                if (entity.Roster.SingleOrDefault(e => e.UserID == _userID) != null)
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

        public ICollection<Profile> AddAthleteToRosterByProfileID (int ProfileID, int TeamID)
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
                        .Where(e => e.TeamID == TeamID)
                        .Single().Roster;

                queryTwo.Add(query);
                ctx.SaveChanges();
                return queryTwo.ToList();
            }
        }
        
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


        public ICollection<Profile> RemoveAthleteFromRosterByProfileID(int ProfileID, int TeamID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Team
                        .Where(e => e.TeamID == TeamID)
                        .Single().Roster;

                var queryTwo =
                   ctx
                        .Profile
                        .Where(e => e.ProfileID == ProfileID)
                        .Single();

                query.Remove(queryTwo);
                ctx.SaveChanges();
                return query.ToList();
            }
        }

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
                var entity =
                    ctx
                        .Team
                        .Single(e => e.TeamID == model.TeamID && e.UserID == _userID);

                entity.TeamName = model.TeamName;
                entity.Roster = model.Roster;
                entity.TeamEvents = model.TeamEvents;
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
