using System;
using System.Collections.Generic;
using System.Text;
using DemoCommon.Models;
namespace DemoCommon.ResModels
{
    public class CommentResponse
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string FullName { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; } 
        public CommentResponse() { }
        public CommentResponse(Comment comment)
        {
            CommentId = comment.CommentId;
            Content = comment.Content;
            FullName=comment.User.LastName+ " "+comment.User.FirstName;
            UserId = comment.UserId;
            PostId = comment.PostId;
        }
    }
}
