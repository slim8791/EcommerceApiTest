using System.ComponentModel.DataAnnotations;

namespace EcommerceApiTest.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "user name required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "password required")]
        public string? Password { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "email required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "first name required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "last name required")]
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; }
    }
}
