using Domain.Entities;

namespace WebApi.ViewModel.MatriculaViewModel
{
    public class CreateMatriculaViewModel
    {
        public DateTime  DataCadastro { get; set; }
        public bool Status { get; set; }

        public int AlunoId { get; set; }

        public int PlanoId { get; set; }
        public int PagamentoId { get; set; }

    }
}