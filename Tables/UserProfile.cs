using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpiAlumni.Tables;

[Table("UserProfile")]
public class UserProfile
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [Column("InstituteId")]
    public int UserId { get; set; }

    [Required]
    [Column("FirstName")]
    [MaxLength(300)]
    public string FirstName { get; set; } = "";

    [Required]
    [Column("LastName")]
    [MaxLength(300)]
    public string LastName { get; set; } = "";

    [Required]
    [Column("UserName")]
    [MaxLength(100)]
    public string UserName { get; set; } = "";

    [Required]
    [Column("Bio")]
    [MaxLength(1000)]
    public string Bio { get; set; } = "";

    [Required]
    [Column("Role")]
    [MaxLength(100)]
    public string Role { get; set; } = "";

    [Required]
    [Column("Password")]
    [MaxLength(512)]
    public string Password { get; set; } = "";

    [Required]
    [Column("PrimaryEmail")]
    [MaxLength(50)]
    public string PrimaryEmail { get; set; } = "";

    [Required]
    [Column("PrimaryPhone")]
    [MaxLength(20)]
    public string PrimaryPhone { get; set; } = "";

    [Required]
    [Column("PrimaryAddress")]
    [MaxLength(1000)]
    public string PrimaryAddress { get; set; } = "";

    [Required]
    [Column("Gender")]
    [MaxLength(20)]
    public string Gender { get; set; } = "";

    [Required]
    [Column("DateOfBirth")]
    public int DateOfBirth { get; set; }
    
    [Required]
    [Column("PhotoUrl")]
    [MaxLength(500)]
    public string PhotoUrl { get; set; } = "";
    
    [Required]
    [Column("SuspensionReason")]
    [MaxLength(300)]
    public string SuspensionReason { get; set; } = "";
    
    [Required]
    [Column("ResetCode")]
    [MaxLength(50)]
    public string ResetCode { get; set; } = "";
    
    [Required]
    [Column("TfaEmail")]
    [MaxLength(300)]
    public string TfaEmail { get; set; } = "";
    
    [Required]
    [Column("TfaCode")]
    [MaxLength(300)]
    public string TfaCode { get; set; } = "";
    
    [Required]
    [Column("TfaSecretExpire")]
    public int TfaSecretExpire { get; set; }  
    
    [Required]
    [Column("TfaSecret")]
    [MaxLength(300)]
    public string TfaSecret { get; set; } = "";
    
    [Required]
    [Column("TfaEnabled")]
    public int TfaEnabled { get; set; }

    [Required]
    [Column("Currency")]
    [MaxLength(20)]
    public string Currency { get; set; } = "";

    [Required]
    [Column("Status")]
    [MaxLength(20)]
    public string Status { get; set; } = "";

    [Required]
    [Column("Creator")]
    public int Creator { get; set; }

    [Required]
    [Column("IpString")]
    [MaxLength(64)]
    public string IpString { get; set; } = "";

    [Required] 
    [Column("CreatedAt")] 
    public long CreatedAt { get; set; }
    
    [Required] 
    [Column("LastLoginAt")] 
    public long LastLoginAt { get; set; }

    [Required] 
    [Column("UpdatedAt")] 
    public long UpdatedAt { get; set; }

    [Required] 
    [Column("DeletedAt")] 
    public long DeletedAt { get; set; }
}