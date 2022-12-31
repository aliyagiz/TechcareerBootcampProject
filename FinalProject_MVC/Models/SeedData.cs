using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinalProject_MVC.Data;
using System;
using System.Linq;

namespace FinalProject_MVC.Models
{
	public class SeedData
	{

		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new ShoppingAppDbContext(
				serviceProvider.GetRequiredService<
					DbContextOptions<ShoppingAppDbContext>>()))
			{
				
				if (context.Users.Any())
				{
					return;   //Already done
				}
				context.Users.AddRange(
					new User
					{
						Name = "Arşimet",
						Surname = "Archimedes",
						Email= "archimedes@hotmail.com",
						Password = "archimedes",
						CreatedDate = DateTime.Now,
						isAdmin= true,
						isActive= true,
					}
				);
				context.SaveChanges();
			}
		}


	}
}
