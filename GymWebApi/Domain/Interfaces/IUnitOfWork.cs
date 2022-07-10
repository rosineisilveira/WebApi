namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
         Task CommitAsync();

         IAlunoRepository AlunoRepository {get;}
         IInstrutorRepository InstrutorRepository {get;}
         IMatriculaRepository MatriculaRepository {get;}
         ITreinoRepository TreinoRepository {get;}
         IExercicioRepository ExercicioRepository {get;}
         IPlanoRepository PlanoRepository {get;}
         IPagamentoRepository PagamentoRepository {get;}
    }
}