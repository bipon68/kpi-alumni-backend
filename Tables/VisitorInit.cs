using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpiAlumni.Tables;

[Table("VisitorInit")]
public class VisitorInit
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [Column("InitId")]
    [MaxLength(32)]
    public string InitId { get; set; } = "";

    [Required]
    [Column("UserId")]
    public int UserId { get; set; }

    [Required]
    [Column("AccessAt")]
    public long AccessAt { get; set; }

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