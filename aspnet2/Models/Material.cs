using System.Collections.Generic;

namespace aspnet2.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Product> Products { get; set; }

        public Material()
        {
            Products = new List<Product>();
        }
    }
}
