
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
    }
}
