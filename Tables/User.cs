
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpiAlumni.Tables
{

    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
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
        public string Username {  get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty; // Use hashed passwords

        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number is invalid.")]
        public string Phone { get; set; } = string.Empty;

        public string Department { get; set; }

        [Range(1900, 2100)]
        public int PassingYear { get; set; }

        // Current job position or occupation
        public string CurrentPosition { get; set; }

        [Required]
        public bool AcceptTermsAndConditions { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Session { get; set; }

        public bool IsAdmin { get; set; } = false; // Renamed for clarity

        [Url]
        public string PhotoUrl { get; set; } = string.Empty;

        [Required]
        public UserStatus Status { get; set; } = UserStatus.Active; // Enum for predefined states

        [MaxLength(45)] // IPv6 addresses can be up to 45 characters
        public string IpAddress { get; set; } = string.Empty;

        public int CreatedById { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }

    public enum UserStatus
    {
        Active,
        Inactive,
        Suspended
    }
}

