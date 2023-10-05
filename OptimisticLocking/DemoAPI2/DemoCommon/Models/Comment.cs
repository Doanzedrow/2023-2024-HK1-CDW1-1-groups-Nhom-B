using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoCommon.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
