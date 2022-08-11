using System.ComponentModel.DataAnnotations;

namespace EcommerceApiTest.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "user name required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string? Password { get; set; }
    }

    public class ResetPasswordModel : LoginModel
    {

    }
    public class ChangePasswordModel : LoginModel
    {
        [Required(ErrorMessage = "Password required")]
        public string NewPassword { get; set; }
    }
}
