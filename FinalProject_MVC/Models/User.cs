using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_MVC.Models

{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        [DefaultValue(false)]
        public bool isAdmin { get; set; }

        [DefaultValue(true)]
        public bool isActive { get; set; }

        //NavigationProperties

        public List<ShoppingList> ShoppingLists { get; set; }
    }
}
