using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private DataContext _context;

        public MatriculaRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Matricula matricula)
        {
             _context.DbSetMatricula.Add(matricula);
        }

        public bool Delete(int matriculaId)
        {
            var matricula = _context.DbSetMatricula.FirstOrDefault(i => i.Id == matriculaId);
            
            if(matricula == null )
                return false;
           else if(matricula.Status == true)
                return false;
            else
            {
                _context.DbSetMatricula.Remove(matricula);
                return true;
            }
        }

        public async Task<IList<Matricula>> GetAllAsync()
        {
             return await _context.DbSetMatricula.Include(i => i.Aluno)
                                                 .Include(i => i.Treinos)
                                                 .Include(i => i.Pagamento)
                                                 .Include(i => i.Plano)
                                                 .ToListAsync();
        }

        public async Task<Matricula> GetByIdAsync(int matriculaId)
        {
            return await _context.DbSetMatricula.Include(i => i.Aluno)
                                                .Include(i => i.Treinos)
                                                .Include(i => i.Pagamento)
                                                .Include(i => i.Plano)
                                                .FirstOrDefaultAsync(x => x.Id == matriculaId);
        }

        public void Update(Matricula matricula)
        {
             _context.Entry(matricula).State = EntityState.Modified;
        }
    }
}