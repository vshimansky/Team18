using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirusAlertAPI.DB
{
    [Table("Limits")]
    public class Limit
    {
        [Key]
        public int lmId { get; set; }
        public string lmImage { get; set; }
        public string lmDesc { get; set; }
    }
}