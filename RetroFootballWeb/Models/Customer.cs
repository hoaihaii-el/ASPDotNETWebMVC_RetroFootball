using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models
{
    public class Customer
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
