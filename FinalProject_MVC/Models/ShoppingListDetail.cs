using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Models
{
    public class ShoppingListDetail
    {
        [Key]
        public int ShoppingListDetailId { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        [DefaultValue(false)]
        public bool isBought { get; set; }

        [DefaultValue(true)]
        public bool isActive { get; set; }

        //Navigation Properties

        public int ItemId { get; set; }
        public Item Item { get; set; }


        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
    }
}
