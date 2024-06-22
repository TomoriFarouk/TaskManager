using System;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Interface.Command.Base;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repository.Command.Base
{
	public class CommandRepository<T> :ICommandRepository<T>where T:class
	{
        private readonly ApplicationDbContext _context;

        public CommandRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync (T entity)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}

