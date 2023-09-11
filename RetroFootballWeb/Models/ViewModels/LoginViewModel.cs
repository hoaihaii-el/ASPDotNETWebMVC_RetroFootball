using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Models.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter your Username!")]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Enter your Password!")]
        public string Password { get; set; }
        public string ReturnURL { get; set; }
    }
}
