using WebApi.ViewModel.ExercicioViewModel;

namespace WebApi.ViewModel.TreinoViewModel
{
    public class ExercicioTreinoViewModel
    {
        public int TreinoId { get; set; }
       //public int ExercicioId { get; set; }
        public IList<InsertExercicioViewModel> Exercicios { get; set; }
    }
}