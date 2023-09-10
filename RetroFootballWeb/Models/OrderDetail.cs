using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        [Key]
        public string ProductID { get; set; }
        [Key]
        public string Size { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
