using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Models
{
    public class ShoppingList
    {
        [Key]
        public int ShoppingListId { get; set; }

        [Required]
        public string ShoppingListName { get; set; }

        public string Description { get; set; }

        [DefaultValue(false)]
        public bool isLocked { get; set; }

        [DefaultValue(true)]
        public bool isActive { get; set; }

        //Navigation Properties
        public List<ShoppingListDetail> ShoppingListDetails { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
