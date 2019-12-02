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
    public class EventController : ApiController
    {
        private EventService CreateEventService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var eventService = new EventService(userId);
            return eventService;
        }
        [HttpGet]
        public IHttpActionResult GetAllEvents(int TeamID)
        {
            EventService eventService = CreateEventService();
            var events = eventService.GetEventsByTeam(TeamID);
            return Ok(events);
        }
        public IHttpActionResult GetEvent(int EventID, int TeamID)
        {
            EventService eventService = CreateEventService();
            var events = eventService.GetEventById(EventID, TeamID);
            return Ok(events);
        }
        public IHttpActionResult Post(EventCreate eventCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateEventService();
            if (!service.CreateEvent(eventCreate))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Put(EventEdit eventEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateEventService();
            if (!service.UpdateEvent(eventEdit))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Delete(int EventID)
        {
            var service = CreateEventService();
            if (!service.DeleteEvent(EventID))
                return InternalServerError();
            return Ok();
        }
    }
}
