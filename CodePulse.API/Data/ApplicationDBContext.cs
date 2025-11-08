using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CodePulse.API.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<BlogImage> BlogImages { get; set; }
	}
}
