using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private DataContext _context;

        public AlunoRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Aluno aluno)
        {
             _context.DbSetAluno.Add(aluno);
        }

        public bool Delete(int alunoId)
        {
            var aluno = _context.DbSetAluno.FirstOrDefault(i => i.Id == alunoId);
            
            if(aluno == null)
                return false;
            else
            {
                _context.DbSetAluno.Remove(aluno);
                return true;
            }
            
        }

        public async Task<IList<Aluno>> GetAllAsync()
        {
            return await _context.DbSetAluno.ToListAsync();
        }

        public async Task<Aluno> GetByIdAsync(int alunoId)
        {
             return await _context.DbSetAluno
                .FirstOrDefaultAsync(x => x.Id == alunoId);
        }

       

        public void Update(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
        }
    }
}