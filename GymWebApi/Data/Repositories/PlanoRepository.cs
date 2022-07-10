using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PlanoRepository : IPlanoRepository
    {
        private DataContext _context;

        public PlanoRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Plano plano)
        {
             _context.DbSetPlano.Add(plano);
        }

        public bool Delete(int planoId)
        {
            var plano = _context.DbSetPlano.FirstOrDefault(i => i.Id == planoId);
            
            if(plano == null)
                return false;
            else
            {
                _context.DbSetPlano.Remove(plano);
                return true;
            }
        }

        public async Task<IList<Plano>> GetAllAsync()
        {
            return await _context.DbSetPlano.ToListAsync();
        }

        public async Task<Plano> GetByIdAsync(int planoId)
        {
            return await _context.DbSetPlano
                .FirstOrDefaultAsync(x => x.Id == planoId);
        }

        public void Update(Plano plano)
        {
           _context.Entry(plano).State = EntityState.Modified;
        }
    }
}