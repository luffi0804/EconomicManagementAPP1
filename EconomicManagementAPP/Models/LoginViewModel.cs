using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    //Hacemos las validaciones requeridas
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Invalid format Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
