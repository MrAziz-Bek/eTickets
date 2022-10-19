using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Base;

public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T: class, IEntityBase, new()
{
    private readonly AppDbContext _context;

    public EntityBaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<T> UpdateAsync(int id, T entity)
    {
        throw new NotImplementedException();
    }
}