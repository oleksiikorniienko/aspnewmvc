using System;
using System.Collections.Generic;
using System.Linq;
using aspnet2.Models;

namespace aspnet2.Services
{
    public class CartService
    {
        public CartService()
        {
        }

        public string Add(CartAddView model, string cartJson)
        {
            if (string.IsNullOrEmpty(cartJson))
            {
                Cart cart = new Cart();
                CartItem cartItem = new CartItem
                {
                    ProductId = int.Parse(model.ProductId),
                    Count = int.Parse(model.Count),
                };

                cart.CartItems.Add(cartItem);

                return Newtonsoft.Json.JsonConvert.SerializeObject(cart);
            }
            else
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);
                bool updated = false;
            
                for (var i = 0; i < cart.CartItems.Count; i++)
                {
                    var item = cart.CartItems.ElementAt(i);
                    if(item.ProductId == int.Parse(model.ProductId))
                    {
                        cart.CartItems.Remove(item);
                        item.Count += int.Parse(model.Count);
                        cart.CartItems.Add(item);
                        updated = true;
                        break;
                    }
                }

                if(!updated)
                {
                    CartItem cartItem = new CartItem
                    {
                        ProductId = int.Parse(model.ProductId),
                        Count = int.Parse(model.Count),
                    };

                    cart.CartItems.Add(cartItem);
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(cart);
            }
        }

        public int[] getProductIds(string cartJson)
        {
            List<int> ids = new List<int>();

            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);

                foreach (CartItem item in cart.CartItems)
                {
                    ids.Add(item.ProductId);
                }
            }

            return ids.ToArray();
        }

        public int GetCount(string cartJson)
        {
            int count = 0;

            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);

                foreach(CartItem item in cart.CartItems)
                {
                    count += item.Count;
                }
            }

            return count;
        }

        public int GetPrice(string cartJson)
        {
            int price = 0;

            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);
                ApplicationContext db = new ApplicationContext();

                foreach (CartItem item in cart.CartItems)
                {
                    Product product = db.Products.Find(item.ProductId);
                    price += product.Price * item.Count;
                }
            }

            return price;
        }

        public string Delete(int productId, string cartJson)
        {
            if (!string.IsNullOrEmpty(cartJson))
            { 
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);

                for (var i = 0; i < cart.CartItems.Count; i++)
                {
                    var item = cart.CartItems.ElementAt(i);
                    if (item.ProductId == productId)
                    {
                        cart.CartItems.Remove(item);
                        break;
                    }
                }

                if(cart.CartItems.Count == 0)
                {
                    return null;
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(cart);
            }

            return cartJson;
        }
    }
}
