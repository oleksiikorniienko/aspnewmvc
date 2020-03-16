using System.Collections.Generic;

namespace aspnet2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
    }
}
