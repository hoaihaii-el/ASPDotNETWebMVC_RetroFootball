using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models
{
    public class WishList
    {
        [Key]
        public string CustomerId { get; set; }

        [Key]
        public string ProductId { get; set; }

        // Đối tượng liên quan đến Customer
        public Customer Customer { get; set; }

        // Đối tượng liên quan đến Product
        public Product Product { get; set; }
    }
}
