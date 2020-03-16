using System.ComponentModel.DataAnnotations;

namespace aspnet2.Models
{
    public class CartAddView
    {
        [Required(ErrorMessage = "ProductId is required")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Count is required")]
        public string Count { get; set; }

    }
}
