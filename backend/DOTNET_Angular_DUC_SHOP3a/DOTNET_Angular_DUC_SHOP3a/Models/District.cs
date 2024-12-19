using System.ComponentModel.DataAnnotations;

namespace DOTNET_Angular_DUC_SHOP3a.Models
{
    public class District
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ProvinceCity ProvinceCity { get; set; }
    }
}
