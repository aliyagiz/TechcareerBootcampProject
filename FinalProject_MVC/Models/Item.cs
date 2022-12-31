using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DefaultValue("https://productimages.hepsiburada.net/s/55/1500/11207234551858.jpg")]
        public string ImageLink { get; set; }

        [DefaultValue(true)]
        public bool isActive { get; set; }

        //Navigation Properties
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
