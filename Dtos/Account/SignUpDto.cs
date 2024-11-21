using System.ComponentModel.DataAnnotations;

namespace KpiAlumni.Dtos.Account
{
    public class SignUpDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)] // Limit to 50 characters
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty; // Use hashed passwords
      
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number is invalid.")]
        public string Phone { get; set; } = string.Empty;

        public string Department { get; set; }

        [Range(1900, 2100)]
        public int PassingYear { get; set; }
        public string Session { get; set; }

        // Current job position or occupation
        public string CurrentPosition { get; set; }

        [Required]
        public bool AcceptTermsAndConditions { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
