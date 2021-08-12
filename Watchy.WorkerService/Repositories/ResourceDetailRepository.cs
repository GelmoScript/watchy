using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Watchy.WorkerService.Contexts;
using Watchy.WorkerService.Models;

namespace Watchy.WorkerService.Repositories
{
	public class ResourceDetailRepository
	{
		private readonly WatchyDbContext _context;

		public ResourceDetailRepository(WatchyDbContext context)
			=> (_context) = (context);

		public async Task<ResourceDetail> Create(ResourceDetail entity)
		{
			var valueTask = await _context.ResourceDetails.AddAsync(entity);
			try
			{
				await valueTask.Context.SaveChangesAsync();
			}
			catch (DbUpdateException e)
			{
				Console.WriteLine(e.Message);
			}
			return entity;
		}
	}
}
