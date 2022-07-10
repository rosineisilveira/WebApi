using Data.Context;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
       private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IAlunoRepository _AlunoRepository;
        private IInstrutorRepository _InstrutorRepository;
        private ITreinoRepository _TreinoRepository;
        private IMatriculaRepository _MatriculaRepository;
        private IPlanoRepository _PlanoRepository;
        private IExercicioRepository _ExercicioRepository;
        private IPagamentoRepository _PagamentoRepository;

       

        public IAlunoRepository AlunoRepository
        {
            get { return _AlunoRepository ??= new AlunoRepository(_context);}
        }

        public IInstrutorRepository InstrutorRepository 
        {
            get { return _InstrutorRepository ??= new InstrutorRepository(_context);}
        }

        public IMatriculaRepository MatriculaRepository
        {
            get { return _MatriculaRepository ??= new MatriculaRepository(_context);}

        }

        public ITreinoRepository TreinoRepository 
        {
            get { return _TreinoRepository ??= new TreinoRepository(_context);}

        }

        public IExercicioRepository ExercicioRepository
        {
            get { return _ExercicioRepository ??= new ExercicioRepository(_context);}

        }

        public IPlanoRepository PlanoRepository 
        {
            get { return _PlanoRepository ??= new PlanoRepository(_context);}

        }
         public IPagamentoRepository PagamentoRepository 
        {
            get { return _PagamentoRepository ??= new PagamentoRepository(_context);}

        }
    }
}