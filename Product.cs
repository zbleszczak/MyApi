using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("products")]
    public class Product
    {
        [Key, Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Brand { get; set; } = string.Empty;
        
        public string Size { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
    }
}
