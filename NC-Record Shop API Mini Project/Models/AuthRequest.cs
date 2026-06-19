using System.ComponentModel.DataAnnotations;

namespace NC_Record_Shop_API_Mini_Project.Models
{
    public class AuthRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
