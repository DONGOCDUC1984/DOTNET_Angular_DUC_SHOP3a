using System.ComponentModel.DataAnnotations;

namespace DOTNET_Angular_DUC_SHOP3a.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
