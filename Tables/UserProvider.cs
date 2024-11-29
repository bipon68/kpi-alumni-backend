using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpiAlumni.Tables;

[Table("UserProvider")]
public class UserProvider
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [Column("UserId")]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Provider")]
    public string Provider { get; set; } = "";

    [Required]
    [MaxLength(255)]
    [Column("LocalProvider")]
    public string LocalProvider { get; set; } = "";

    [Required]
    [MaxLength(255)]
    [Column("DisplayName")]
    public string DisplayName { get; set; } = "";

    [Required]
    [MaxLength(20)]
    [Column("IdentityType")]
    public string IdentityType { get; set; } = "";

    [Required]
    [MaxLength(255)]
    [Column("Identity")]
    public string Identity { get; set; } = "";

    [Required]
    [Column("IsVerified")]
    public bool IsVerified { get; set; }

    [Required]
    [MaxLength(500)]
    [Column("PhotoUrl")]
    public string PhotoUrl { get; set; } = "";

    [Required]
    [MaxLength(255)]
    [Column("ProviderUid")]
    public string ProviderUid { get; set; } = "";

    [Required]
    [MaxLength(100)]
    [Column("UserUid")]
    public string UserUid { get; set; } = "";

    [Required]
    [Column("IsHide")]
    public bool IsHide { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Status")]
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
    [Column("UpdatedAt")]
    public long UpdatedAt { get; set; }

    [Required]
    [Column("DeletedAt")]
    public long DeletedAt { get; set; }
}