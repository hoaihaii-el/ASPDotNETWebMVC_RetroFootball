using RetroFootballWeb.Repository;
using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models
{
    public class Cart
    {
        [Key]
        public string CustomerId { get; set; }

        [Key]
        public string ProductId { get; set; }
        [Key]
        public string Size { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Đối tượng liên quan đến Customer
        public Customer Customer { get; set; }

        // Đối tượng liên quan đến Product
        public Product Product { get; set; }
    }
}
