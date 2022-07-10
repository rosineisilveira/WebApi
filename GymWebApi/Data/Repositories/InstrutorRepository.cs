using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class InstrutorRepository : IInstrutorRepository
    {
        private DataContext _context;

        public InstrutorRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Instrutor instrutor)
        {
            _context.DbSetInstrutor.Add(instrutor);
        }

        public bool Delete(int instrutorId)
        {
            var instrutor = _context.DbSetInstrutor.FirstOrDefault(i => i.Id == instrutorId);
            
            if(instrutor == null)
                return false;
            else
            {
                _context.DbSetInstrutor.Remove(instrutor);
                return true;
            }
        }

        public async Task<IList<Instrutor>> GetAllAsync()
        {
            return await _context.DbSetInstrutor.ToListAsync();
        }

        public async Task<Instrutor> GetByIdAsync(int instrutorId)
        {
            return await _context.DbSetInstrutor
                .FirstOrDefaultAsync(x => x.Id == instrutorId);
        }

        public void Update(Instrutor instrutor)
        {
            _context.Entry(instrutor).State = EntityState.Modified;
        }
    }
}