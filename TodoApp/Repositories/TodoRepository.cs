using TodoApp.Repositories.Interfaces;
using TodoApp.Models;
using TodoApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Repositories
{
    public class TodoRepository: ITodoRepository
    {
        private readonly ApiDbContext _context;
        
        public TodoRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task Create(ItemData item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ItemData>> GetAll()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<ItemData> GetById(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(z => z.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ItemData item)
        {
            _context.Remove(item);
            await this.Save();
        }
    }
}
