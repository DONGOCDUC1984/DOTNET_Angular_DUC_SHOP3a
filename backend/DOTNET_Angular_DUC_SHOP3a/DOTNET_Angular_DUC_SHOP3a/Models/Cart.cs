
namespace DOTNET_Angular_DUC_SHOP3a.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
