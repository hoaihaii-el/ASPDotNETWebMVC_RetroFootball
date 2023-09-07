using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        [Key]
        public string CustomerID { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Customer Customer { get; set; }
    }
}
