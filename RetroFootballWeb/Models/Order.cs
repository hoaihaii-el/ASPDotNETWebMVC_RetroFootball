using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RetroFootballWeb.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public string CustomerID { get; set; }
        [Required]
        public DateTime Time_create { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public string Pay_method { get; set; }
        [Required]
        public DateTime Delivery_date { get; set; }
        [Required]
        public string Delivery_method { get; set; }
        [Required]
        public string Status { get; set; }
        public string Note { get; set; }
        public Customer Customer { get; set; }
    }
}
