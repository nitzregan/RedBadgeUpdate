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
    public class ProfileController : ApiController
    {
        public IHttpActionResult GetAll(int TeamID)
        {
            ProfileService profileService = CreateProfileService();
            var profile = profileService.GetAllProfilesByTeam(TeamID);
            return Ok(profile);
        }

        public IHttpActionResult Get(Guid UserID)
        {
            ProfileService profileService = CreateProfileService();
            var profile = profileService.GetProfile(UserID);
            return Ok(profile);
        }

        public IHttpActionResult Put(ProfileEdit profile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateProfileService();

            if (!service.UpdateProfile(profile))
                return InternalServerError();

            return Ok();
        }

        private ProfileService CreateProfileService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var profileService = new ProfileService(userID);
            return profileService;
        }
    }
}
