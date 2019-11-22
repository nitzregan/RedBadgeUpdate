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
    public class CommentController : ApiController
    {
        private CommentService CreateCommentService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var CommentService = new CommentService(userID);
            return CommentService;
        }
        public IHttpActionResult Post(CommentCreate commentCreate, int ProfileID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateCommentService();
            if (!service.CreateComment(commentCreate, ProfileID))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult GetEvent(int CommentID, int ProfileID)
        {
            CommentService commentService = CreateCommentService();
            var comments = commentService.GetCommentById(CommentID, ProfileID);
            return Ok(comments);
        }

        public IHttpActionResult Delete(int CommentID, int ProfileID)
        {
            var service = CreateCommentService();
            if (!service.DeleteComment(CommentID, ProfileID))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult GetAllComments(int ProfileID)
        {
            CommentService commentService = CreateCommentService();
            var comments = commentService.GetCommentsByProfile(ProfileID);
            return Ok(comments);
        }
    }
}
