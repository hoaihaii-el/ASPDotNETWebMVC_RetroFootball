using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models
{
    public class Customer
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Male { get; set; }
    }
}
