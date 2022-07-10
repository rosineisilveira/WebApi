using Domain.Entities;

namespace WebApi.ViewModel.TreinoViewModel
{
    public class UpdateTreinoViewModel
    {
        public string Nome { get; set; }

        public Instrutor Instrutor { get; set; }
        public int InstrutorId { get; set; }

      
    }
}