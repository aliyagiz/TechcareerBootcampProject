using FinalProject_MVC.Data;
using FinalProject_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace FinalProject_MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			bool cookieExist = Request.Cookies["User"] is null ? false : true;
			if (cookieExist)
			{
				string email = Request.Cookies["Email"];
				using (var context = new ShoppingAppDbContext())
				{
					User user = context.Users.Where(x => x.Email == email).FirstOrDefault();

					return View(user);
				}
			}
			else
			{
				return View();
			}

		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}


		[HttpPost]
		public IActionResult LoginFunction(string email, string password)
		{
			using (var context = new ShoppingAppDbContext())
			{
				List<User> users = context.Users.ToList();
				User user = users.Where(x => x.Email == email).FirstOrDefault();

				if (user is null)
				{
					//Return user-not-found
					TempData["NotificationMsg"] = "User not found!";
					return RedirectToAction("Login");
				}

				if (user.Password == password)
				{
					//access granted

					bool cookieExist = Request.Cookies["User"] is null ? false : true;

					if (cookieExist)
					{
						RemoveCookie("User");
						RemoveCookie("Email");
						RemoveCookie("Auth");
					}

					SetCookie("User", user.Name + " " + user.Surname, 30);
					SetCookie("Email", user.Email.ToString(), 30);
					SetCookie("Auth", user.isAdmin.ToString(), 30);

				}
				else
				{
					TempData["NotificationMsg"] = "Password wrong!";
					return RedirectToAction("Login");
				}

				return View("Index", user);
			}
		}

		public IActionResult Register()
		{
			return View();
		}


		public IActionResult Logout()
		{
			RemoveCookie("User");
			RemoveCookie("Email");
			RemoveCookie("Auth");

			return View("Index");
		}

		[HttpPost]
		public IActionResult RegisterFunction(string name, string surname, string email, string password, string password2)
		{
			using (var context = new ShoppingAppDbContext())
			{
				if (name is null || surname is null || email is null || password is null || password2 is null)
				{
					TempData["NotificationMsg"] = "Please fill the required areas!";
					return RedirectToAction("Register");
				}

				List<User> users = context.Users.ToList();
				User user = users.Where(x => x.Email == email).FirstOrDefault();

				if (user is not null)
				{
					//Return email already in use
					TempData["NotificationMsg"] = "Email already taken!";
					return RedirectToAction("Register");
				}

				if (!EmailCheck(email))
				{
					TempData["NotificationMsg"] = "Mail is in invalid form!";
					return RedirectToAction("Register");
				}

				if (password != password2)
				{
					//Return pass2-wrong
					TempData["NotificationMsg"] = "Passwords not matching!";
					return RedirectToAction("Register");
				}

				if (!PasswordCheck(password))
				{
					TempData["NotificationMsg"] = "Password does not match requirements!";
					return RedirectToAction("Register");
				}



				User newUser = new User
				{
					Name = name,
					Surname = surname,
					Email = email,
					Password = password,
					isActive = true,
					isAdmin = false,
					CreatedDate = DateTime.Now,
				};

				context.Add(newUser);
				context.SaveChanges();

				bool cookieExist = Request.Cookies["User"] is null ? false : true;

				if (cookieExist)
				{
					RemoveCookie("User");
					RemoveCookie("Email");
					RemoveCookie("Auth");
				}

				SetCookie("User", name + " " + surname, 30);
				SetCookie("Email", email, 30);
				SetCookie("Auth", "false", 30);

				return View("Index", newUser);
			}
		}

		public bool PasswordCheck(string password)
		{
			if (password is null)
			{
				return false;
			}
			bool isAcceptable = password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsNumber) && password.Length >= 8;

			return isAcceptable;
		}

		public bool EmailCheck(string email)
		{
			if (email is null)
			{
				return false;
			}
			bool isAcceptable = email.Contains("@") && email.Contains(".com");
			return isAcceptable;
		}

		public void SetCookie(string key, string value, int? expireTime)
		{
			CookieOptions option = new CookieOptions();

			if (expireTime.HasValue)
				option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
			else
				option.Expires = DateTime.Now.AddMilliseconds(10);

			option.HttpOnly = true;

			Response.Cookies.Append(key, value, option);
		}

		public void RemoveCookie(string key)
		{
			Response.Cookies.Delete(key);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public List<Item> GetAllItems()
		{
			using (var context = new ShoppingAppDbContext())
			{
				List<Item> allItems = context.Items.ToList();

				return allItems;
			}
		}
		public List<Category> GetAllCategories()
		{
			using (var context = new ShoppingAppDbContext())
			{
				List<Category> allCategories = context.Categories.ToList();

				return allCategories;
			}
		}

		public List<ShoppingList> GetShoppingList(int userId)
		{
			using (var context = new ShoppingAppDbContext())
			{
				
				List<ShoppingList> shoppingList = context.ShoppingList.Where(x=> x.UserId == userId).ToList();

				return shoppingList;
			}
		}
	}
}