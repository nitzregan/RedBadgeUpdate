using Microsoft.AspNet.Identity;
using RedBadge.Model;
using RedBadge.Services;
using System;
using System.Web.Http;

namespace RedBadgeNew.API.Controllers
{
    [Authorize]
    public class TeamController : ApiController
    {
        public IHttpActionResult GetAllForCoach(Guid UserID)
        {
            TeamService teamService = CreateTeamService();
            var team = teamService.GetAllTeamsForCoachByUserID(UserID);
            return Ok(team);
        }

        public IHttpActionResult GetAllForAthlete(Guid UserID)
        {
            TeamService teamService = CreateTeamService();
            var team = teamService.GetAllTeamsForAthleteByUserID(UserID);
            return Ok(team);
        }

        public IHttpActionResult Get(int TeamID)
        {
            TeamService teamService = CreateTeamService();
            var team = teamService.GetTeamById(TeamID);
            return Ok(team);
        }

        public IHttpActionResult Put(TeamEdit team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.UpdateTeam(team))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int TeamId)
        {
            var service = CreateTeamService();

            if (!service.DeleteTeam(TeamId))
                return InternalServerError();

            return Ok();
        }

        private TeamService CreateTeamService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var teamService = new TeamService(userID);
            return teamService;
        }

        public IHttpActionResult Post(TeamCreate teamCreate, int ProfileID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateTeamService();
            if (!service.CreateTeam(teamCreate, ProfileID))
                return InternalServerError();
            return Ok();
        }



        [HttpPut]
        [Route("api/AddPlayer")]
        public IHttpActionResult Put(int ProfileID, int TeamID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.AddAthleteToRosterByProfileID(ProfileID, TeamID))
                return InternalServerError();

            return Ok();
        }

        [HttpPut]
        [Route("api/RemovePlayer")]
        public IHttpActionResult PutBack(int ProfileID, int TeamID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.RemoveAthleteFromRosterByProfileID(ProfileID, TeamID))
                return InternalServerError();

            return Ok();
        }
    }
}

