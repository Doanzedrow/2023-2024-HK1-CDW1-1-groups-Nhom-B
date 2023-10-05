using DemoCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DemoCommon.ReqModels
{
    public class PostRequest
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string? PostImage { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? Like { get; set; } = 0;
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public PostRequest()
        {
        }

        public PostRequest(Post p)
        {
            PostId = p.PostId;
            Content = p.Content;
            GroupId = p.GroupId;
            UserId = p.UserId;
            PostImage = p.PostImage;
            CreatedDateTime = p.CreatedDateTime;
            Like = p.Like;
            ImageFile = p.ImageFile;
        }
    }
}
