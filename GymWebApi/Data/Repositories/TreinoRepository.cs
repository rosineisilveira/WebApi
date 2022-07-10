using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TreinoRepository : ITreinoRepository
    {
        private DataContext _context;

        public TreinoRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Treino treino)
        {
            _context.DbSetTreino.Add(treino);
          
        }

        public bool Delete(int treinoId)
        {
            var treino = _context.DbSetTreino.FirstOrDefault(i => i.Id == treinoId);
            
            if(treino == null)
                return false;
            else
            {
                _context.DbSetTreino.Remove(treino);
                return true;
            }
        }

        public async Task<IList<Treino>> GetAllAsync()
        {
            //return await _context.DbSetTreino.ToListAsync();
            return await _context.DbSetTreino.Include(i => i.Instrutor)
                                             .Include(i => i.Exercicios)
                                             .ToListAsync();
                                             
        }

        public async Task<Treino> GetByIdAsync(int treinoId)
        {
            return await _context.DbSetTreino.Include(i => i.Instrutor)
                                             .Include(i => i.Exercicios)
                                             .FirstOrDefaultAsync(x => x.Id == treinoId);

               
                
        }

        public void Update(Treino treino)
        {
             _context.Entry(treino).State = EntityState.Modified;
        }
    }
}