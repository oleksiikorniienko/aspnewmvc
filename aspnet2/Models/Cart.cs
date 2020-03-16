using System.Collections.Generic;

namespace aspnet2.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
