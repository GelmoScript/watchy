using Microsoft.EntityFrameworkCore;
using Watchy.WorkerService.Models;

namespace Watchy.WorkerService.Contexts
{
	public class WatchyDbContext : DbContext
	{
		public DbSet<ResourceDetail> ResourceDetails { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer(@"Data Source=172.20.10.3,1433;Initial Catalog=WatchyServiceDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			optionsBuilder.UseSqlServer(@"Server=172.20.10.3,1433;Database=WatchyServiceDB;User Id=al;Password=1234");
		}
	}
}
