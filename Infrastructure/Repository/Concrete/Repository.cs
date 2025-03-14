﻿using Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Concrete
{
    // Generic Repository Implementation
    // Generic Repository Implementation
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShippingDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ShippingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

