using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroFootballWeb.Models
{
    public class Product
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }        
        public string Club { get; set; }
        public string Nation { get; set; }
        public string Season { get; set; }
        public decimal Price { get; set; }
        public int Size_s { get; set; }
        public int Size_m { get; set; }
        public int Size_l { get; set; }
        public int Size_xl { get; set; }
        public string Status { get; set; }
        public DateTime Time_added { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [FileExtensions]
        public IFormFile ImageUpLoad { get; set; }
    }
}
