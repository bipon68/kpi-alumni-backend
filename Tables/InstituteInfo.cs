using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpiAlumni.Tables;

[Table("InstituteInfo")]
public class InstituteInfo
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [Required]
    [Column("Title")]
    [MaxLength(255)]
    public string Title { get; set; } = "";

    [Required]
    [Column("Slogan")]
    [MaxLength(255)]
    public string Slogan { get; set; } = "";
    
    [Required]
    [Column("Type")]
    [MaxLength(255)]
    public string Type { get; set; } = "";

    [Required]
    [Column("CampusSize")]
    [MaxLength(255)]
    public string CampusSize { get; set; } = "";
    
    [Required]
    [Column("LogoUrl")]
    [MaxLength(500)]
    public string LogoUrl { get; set; } = "";

    [Required]
    [Column("Website")]
    [MaxLength(500)]
    public string Website { get; set; } = "";
    
    [Required]
    [Column("Location")]
    [MaxLength(255)]
    public string Location { get; set; } = "";
    
    
    [Required]
    [Column("Address")]
    public string Address { get; set; } = "";

    [Required]
    [Column("Status")]
    [MaxLength(20)]
    public string Status { get; set; } = "";
    
    [Required]
    [Column("IpString")]
    [MaxLength(64)]
    public string IpString { get; set; } = "";
    
    [Required]
    [Column("Creator")]
    public int Creator { get; set; }

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