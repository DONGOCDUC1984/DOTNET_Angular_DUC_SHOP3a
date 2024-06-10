
namespace DOTNET_Angular_DUC_SHOP3a.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public District District { get; set; }
        public string ImageUrl { get; set; }
    }
}
