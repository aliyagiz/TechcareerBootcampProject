using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [DefaultValue(true)]
        public bool isActive { get; set; }

        //Navigation Properties
        public List<Item> Items { get; set; }
    }
}
