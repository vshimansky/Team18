using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirusAlertAPI.DB
{
    [Table("News")]
    public class News
    {
        [Key]
        public int newId { get; set; }
        public int newRegId { get; set; }
        public string newImgLink { get; set; }
        public string newText { get; set; }
        public string newUrl { get; set; }
        public string newDate { get; set; }
    }

}