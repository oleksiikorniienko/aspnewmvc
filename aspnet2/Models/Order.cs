namespace aspnet2.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }
        public int Sum { get; set; }
    }
}
