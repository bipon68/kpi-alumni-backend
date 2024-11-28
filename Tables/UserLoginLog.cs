using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpiAlumni.Tables;

[Table("UserLoginLog")]
public class UserLoginLog
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("UserId")]
    public int UserId { get; set; }

    [Column("ProviderId")]
    public int ProviderId { get; set; }

    [Required]
    [Column("UserUid")]
    [MaxLength(50)]
    public string UserUid { get; set; } = "";

    [Required]
    [Column("LoginBy")]
    [MaxLength(20)]
    public string LoginBy { get; set; } = "";

    [Required]
    [Column("UniqueKey")]
    [MaxLength(100)]
    public string UniqueKey { get; set; } = "";

    [Required]
    [Column("UserAgent")]
    [MaxLength(255)]
    public string UserAgent { get; set; } = "";

    [Column("ExpiredAt")]
    public long ExpiredAt { get; set; }

    [Column("LogoutAt")]
    public long LogoutAt { get; set; }

    [Required]
    [Column("ChecksumBrowser")]
    [MaxLength(32)]
    public string ChecksumBrowser { get; set; } = "";
    
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
    [Column("UpdatedAt")] 
    public long UpdatedAt { get; set; }

    [Required] 
    [Column("DeletedAt")] 
    public long DeletedAt { get; set; }
}