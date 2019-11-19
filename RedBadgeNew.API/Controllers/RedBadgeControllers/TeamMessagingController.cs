﻿using Microsoft.AspNet.Identity;
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
        public IHttpActionResult Delete(int messageId)
        {
            var service = CreateTeamMessagingService();
            if (!service.DeleteTeamMessage(messageId))
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
        public IHttpActionResult Get(int messageId)
        {
            TeamMessagingService teamMessagingService = CreateTeamMessagingService();
            var teamMessage = teamMessagingService.GetTeamMessageById(messageId);
            return Ok(teamMessage);
        }
        public IHttpActionResult GetAll()
        {
            TeamMessagingService teamMessagingService = CreateTeamMessagingService();
            var teamMessage = teamMessagingService.GetTeamMessages();
            return Ok(teamMessage);
        }
    }
}