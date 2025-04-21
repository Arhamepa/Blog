using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog_CoreLayer.DTOs.User
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Blog_DataLayer.Entities.User.UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; }

    }
}
