using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirusAlertAPI.DB
{
    [Table("Regions")]
    public class Regions
    {
        [Key]
        public int regId { get; set; }
        public string regName { get; set; }
        public int? regParentId { get; set; }
    }
}