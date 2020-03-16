using System;
using System.Collections.Generic;
using System.Linq;
using aspnet2.Models;

namespace aspnet2.Services
{
    public class CartToOrderService
    {
        ApplicationContext db = new ApplicationContext();

        public CartToOrderService()
        {
        }

        public void CartToOrder(string cartJson, int orderId)
        {
            var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);

            foreach (CartItem item in cart.CartItems)
            {
                Product product = db.Products.Find(item.ProductId);
                OrderProducts orderProducts = new OrderProducts
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Count = item.Count,
                    Price = product.Price
                };

                db.OrderProducts.Add(orderProducts);
            }

            db.SaveChanges();
        }
    }
}
