using FinalProject_MVC.Data;
using FinalProject_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject_MVC.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Category()
		{
			string email = Request.Cookies["Email"];

			if (email is null)
			{
				return Unauthorized();
			}

			using (var context = new ShoppingAppDbContext())
			{
				User user = context.Users.Where(x => x.Email == email).FirstOrDefault();

				if (!user.isAdmin)
				{
					return Unauthorized();
				}
				var categories = GetAllCategories();

				return View("Category", categories);
			}
		}

		[HttpPost]
		public IActionResult DeleteCategory(int CategoryId)
		{
			string email = Request.Cookies["Email"];

			if (email is null)
			{
				return Unauthorized();
			}

			using (var context = new ShoppingAppDbContext())
			{
				User user = context.Users.Where(x => x.Email == email).FirstOrDefault();

				if (!user.isAdmin)
				{
					return Unauthorized();
				}

				Category category = context.Categories.Where(x => x.CategoryId == CategoryId).FirstOrDefault();
				category.isActive = false;

				context.SaveChanges();

				return RedirectToAction("Category");
			}
		}

		public IActionResult AddCategory()
		{
			return View();
		}

		public IActionResult InsertCategory(string categoryname)
		{
			string email = Request.Cookies["Email"];

			if (email is null)
			{
				return Unauthorized();
			}

			using (var context = new ShoppingAppDbContext())
			{
				User user = context.Users.Where(x => x.Email == email).FirstOrDefault();

				if (!user.isAdmin)
				{
					return Unauthorized();
				}

				Category category = new Category()
				{
					CategoryName = categoryname,
					isActive = true,
				};

				context.Add(category);
				context.SaveChanges();

				return RedirectToAction("Category");
			}
		}

		public IActionResult Item()
		{
			string email = Request.Cookies["Email"];

			if (email is null)
			{
				return Unauthorized();
			}

			using (var context = new ShoppingAppDbContext())
			{
				User user = context.Users.Where(x => x.Email == email).FirstOrDefault();

				if (!user.isAdmin)
				{
					return Unauthorized();
				}
				var items = GetAllItems();

				return View("Item", items);
			}
		}

		public IActionResult AddItem()
		{
			using (var context = new ShoppingAppDbContext())
			{
				List<SelectListItem> items = context.Categories.Where(x=> x.isActive).Select(x => new SelectListItem(x.CategoryName, x.CategoryId.ToString())).ToList();
				ViewBag.categories = items;


				return View();
			}	
		}

		public IActionResult InsertItem(string itemName, string imageurl, int categoryid)
		{
			string email = Request.Cookies["Email"];

			if (email is null)
			{
				return Unauthorized();
			}

			using (var context = new ShoppingAppDbContext())
			{
				User user = context.Users.Where(x => x.Email == email).FirstOrDefault();

				if (!user.isAdmin)
				{
					return Unauthorized();
				}

				Item item = new Item()
				{
					Name = itemName,
					ImageLink = imageurl,
					CategoryId = categoryid,
					isActive = true,
				};

				context.Add(item);
				context.SaveChanges();

				return RedirectToAction("Item");
			}
		}

		public IActionResult DeleteItem(int itemId)
		{
			string email = Request.Cookies["Email"];

			if (email is null)
			{
				return Unauthorized();
			}

			using (var context = new ShoppingAppDbContext())
			{
				User user = context.Users.Where(x => x.Email == email).FirstOrDefault();

				if (!user.isAdmin)
				{
					return Unauthorized();
				}

				Item item = context.Items.Where(x => x.ItemId == itemId).FirstOrDefault();
				item.isActive = false;

				context.SaveChanges();

				return RedirectToAction("Item");
			}
		}

		[HttpGet]
		public List<Item> GetAllItems()
		{
			using (var context = new ShoppingAppDbContext())
			{
				List<Item> allItems = context.Items.Where(x => x.isActive).ToList();

				return allItems;
			}
		}
		[HttpGet]
		public List<Category> GetAllCategories()
		{
			using (var context = new ShoppingAppDbContext())
			{
				List<Category> allCategories = context.Categories.Where(x => x.isActive).ToList();

				return allCategories;
			}
		}
		[HttpGet]
		public List<ShoppingList> GetShoppingList(int userId)
		{
			using (var context = new ShoppingAppDbContext())
			{

				List<ShoppingList> shoppingList = context.ShoppingList.Where(x => x.UserId == userId && x.isActive).ToList();

				return shoppingList;
			}
		}
	}
}
