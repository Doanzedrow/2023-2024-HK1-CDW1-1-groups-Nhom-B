using DemoCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCommon.ResModels
{
    public class UserResponse
    {
        public int UserId { get;set; }
        public string FullName { get;set; }

        public UserResponse() { }
        public UserResponse(User user)
        {
            UserId = user.UserId;
            FullName = user.LastName + " " + user.FirstName;
        }
    }

}
