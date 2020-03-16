using System.Collections.Generic;

namespace aspnet2.Models
{
    public class UserType
    {
        public const int USER = 1;
        public const int ADMIN = 2;

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }

        public UserType()
        {
            Users = new List<User>();
        }
    }
}
