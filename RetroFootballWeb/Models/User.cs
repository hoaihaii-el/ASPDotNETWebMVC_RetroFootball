using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter your Username!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter your Email"), EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Enter your Password!")]
        public string Password { get; set; }
    }
}
