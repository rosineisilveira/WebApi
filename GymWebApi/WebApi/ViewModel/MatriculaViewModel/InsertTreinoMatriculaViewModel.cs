using WebApi.ViewModel.TreinoViewModel;

namespace WebApi.ViewModel.MatriculaViewModel
{
    public class InsertTreinoMatriculaViewModel
    {
        public int  MatriculaId { get; set; }
        public IList<InsertTreinoViewModel> Treinos { get; set; }
    }
}