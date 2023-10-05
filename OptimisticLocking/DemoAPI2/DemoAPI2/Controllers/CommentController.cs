using DemoCommon.Models;
using DemoCommon.ResModels;
using DemoService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("getAllComment/{postId?}")]
        public async Task<List<CommentResponse>> GetAllComment(int postId)
       {
            var data = await _commentService.GetAllComment(postId);
            var result = data.Select(c => new CommentResponse(c)).ToList();
            return result;
        }
        [HttpGet]
        [Route("getById/{id?}")]
        public async Task<Comment> GetComment(int? id)
        {
            var data = await _commentService.GetComment(id);
            return data;
        }

        [HttpPost]
        [Route("postComment")]
        public async Task<CommentResponse> PostComment([FromBody] Comment comment)
        {
            var data = await _commentService.PostComment(comment);
            var result = await GetComment(data.CommentId);
            return new CommentResponse(result);
        }
        [HttpDelete("{commentId}")]
        public JsonResult Deletepost(int commentId, [FromQuery]int userId)
        {
            _commentService.DeleteComment(commentId,userId);
            return new JsonResult("Deleted Successfully");
        }
    }
}
