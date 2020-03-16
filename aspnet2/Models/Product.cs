namespace aspnet2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int InStock { get; set; }
        public int SizeW { get; set; }
        public int SizeH { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
