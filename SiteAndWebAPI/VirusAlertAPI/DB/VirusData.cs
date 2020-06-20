using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirusAlertAPI.DB
{
    [Table("VirusData")]
    public class VirusData
    {
        [Key]
        public int vdId { get; set; }
        public int vdRegId { get; set; }
        public string vdDate { get; set; }
        public int vdInfect { get; set; }
        public int vdInfectTotal { get; set; }
        public int vdHealth { get; set; }
        public int vdHealthTotal { get; set; }
        public int vdDeath { get; set; }
        public int vdDeathTotal { get; set; }

    }

}