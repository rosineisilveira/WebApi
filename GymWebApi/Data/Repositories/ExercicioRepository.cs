using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ExercicioRepository : IExercicioRepository
    {
        private DataContext _context;

        public ExercicioRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Exercicio exercicio)
        {
            _context.DbSetExercicio.Add(exercicio);
        }

        public bool Delete(int exercicioId)
        {
             var exercicio = _context.DbSetExercicio.FirstOrDefault(i => i.Id == exercicioId);
            
            if(exercicio == null)
                return false;
            else
            {
                _context.DbSetExercicio.Remove(exercicio);
                return true;
            }
        }

        public async Task<IList<Exercicio>> GetAllAsync()
        {
            return await _context.DbSetExercicio.ToListAsync();
        }

        public async Task<Exercicio> GetByIdAsync(int exercicioId)
        {
             return await _context.DbSetExercicio
                .FirstOrDefaultAsync(x => x.Id == exercicioId);
        }

        public void Update(Exercicio exercicio)
        {
            _context.Entry(exercicio).State = EntityState.Modified;
        }
    }
}