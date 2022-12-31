using FinalProject_MVC.Data;
using FinalProject_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FinalProject_MVC.Controllers
{
	public class ShoppingListController : Controller
	{
		public IActionResult Index()
		{
			bool cookieExist = Request.Cookies["User"] is null ? false : true;
			if (cookieExist)
			{
				string email = Request.Cookies["Email"];
				using (var context = new ShoppingAppDbContext())
				{
					User user = context.Users.Where(x => x.Email == email).FirstOrDefault();

					List<ShoppingList> shoppingLists = context.ShoppingList.Where(x => x.isActive && x.UserId == user.UserId).ToList();

					return View(shoppingLists);
				}
			}
			else
			{
				return Unauthorized();
			}

		}

		public IActionResult AddShoppingList()
		{

			return View();
		}
		
		public void DeleteShoppingList()
		{

		}

		public IActionResult InsertShoppingList(string shoppinglistname, string shoppinglistdescription)
		{
			string email = Request.Cookies["Email"];

			if (email is null)
			{
				return Unauthorized();
			}

			using (var context = new ShoppingAppDbContext())
			{
				User user = context.Users.Where(x => x.Email == email).FirstOrDefault();


				ShoppingList list = new ShoppingList()
				{
					ShoppingListName = shoppinglistname,
					Description = shoppinglistdescription,
					isActive = true,
					User = user
				};

				context.Add(list);
				context.SaveChanges();

				return RedirectToAction("Index");
			}			
		}

		[HttpPost]
		public IActionResult AddShoppingListItem(int ListId)
		{
			using (var context = new ShoppingAppDbContext())
			{
				List<SelectListItem> items = context.Items.Where(x => x.isActive).Select(x => new SelectListItem(x.Name, x.ItemId.ToString())).ToList();
				ViewBag.items = items;

				ViewBag.listId = ListId;

				return View();
			}
		}

		public IActionResult InsertShoppingListItem(int itemid, int amount, int listId)
		{
			using (var context = new ShoppingAppDbContext())
			{
				ShoppingList shoppingList = context.ShoppingList.Where(x => x.ShoppingListId == listId).FirstOrDefault();
				Item item = context.Items.Where(x => x.ItemId == itemid).FirstOrDefault();

				ShoppingListDetail shoppingListDetail = new ShoppingListDetail()
				{
					ShoppingList = shoppingList,
					Amount = amount,
					isActive= true,
					isBought= false,
					Item = item,
					Description = ""
				};

				context.Add(shoppingListDetail);
				context.SaveChanges();

				return RedirectToAction("Index");
			}


		}

		public IActionResult ViewShoppingList(int ListId)
		{
			using (var context = new ShoppingAppDbContext())
			{
				List<ShoppingListDetail> shoppingListDetails = context.ShoppingListDetail.Where(x=> x.ShoppingListId == ListId && x.isBought == false && x.isActive == true).Include(x=> x.Item).ToList();

				return View(shoppingListDetails);
			}
		}
		
	}
}
