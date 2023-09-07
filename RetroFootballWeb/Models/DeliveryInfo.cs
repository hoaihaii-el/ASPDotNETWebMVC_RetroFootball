using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models
{
    public class DeliveryInfo
    {
        [Key]
        public string CustomerID { get; set; }
        [Key]
        public int Priority { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Customer Customer { get; set; }
    }
}
