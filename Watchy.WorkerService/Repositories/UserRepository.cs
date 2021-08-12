using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchy.WorkerService.Contexts;
using Watchy.WorkerService.Models;

namespace Watchy.WorkerService.Repositories
{
	public class UserRepository
	{
		private readonly WatchyDbContext _context;
		public UserRepository(WatchyDbContext context)
			=> (_context) = (context);

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _context.Users.ToListAsync();
		}
	}
}
