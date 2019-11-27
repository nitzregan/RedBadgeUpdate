using Microsoft.AspNet.Identity;
using RedBadge.Model;
using RedBadge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedBadgeNew.API.Controllers
{
    [Authorize]
    public class TeamMessagingController : ApiController
    {
        private TeamMessagingService CreateTeamMessagingService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var teamMessagingService = new TeamMessagingService(userID);
            return teamMessagingService;
        }
        public IHttpActionResult Delete(int MessageID)
        {
            var service = CreateTeamMessagingService();
            if (!service.DeleteTeamMessage(MessageID))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Put(TeamMessagingEdit teamMessage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateTeamMessagingService();
            if (!service.UpdateTeamMessage(teamMessage))
                return InternalServerError();
            return Ok();
        }
<<<<<<< HEAD
        public IHttpActionResult Get(int messageId, int TeamID)
        {
            TeamMessagingService teamMessagingService = CreateTeamMessagingService();
            var teamMessage = teamMessagingService.GetTeamMessageById(messageId, TeamID);
=======
        public IHttpActionResult Get(int MessageID)
        {
            TeamMessagingService teamMessagingService = CreateTeamMessagingService();
            var teamMessage = teamMessagingService.GetTeamMessageById(MessageID);
>>>>>>> a8c1fb4577c4ffd31103d68d158f32b4f4c7c167
            return Ok(teamMessage);
        }
        public IHttpActionResult GetAll(int TeamID)
        {
            TeamMessagingService teamMessagingService = CreateTeamMessagingService();
            var teamMessage = teamMessagingService.GetTeamMessages(TeamID);
            return Ok(teamMessage);
        }

        public IHttpActionResult Post(TeamMessagingCreate teamMessagingCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateTeamMessagingService();
            if (!service.CreateTeamMessaging(teamMessagingCreate))
                return InternalServerError();
            return Ok();
        }
    }
}
