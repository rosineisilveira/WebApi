using Domain.Entities;

namespace WebApi.DTO.TreinoDTO
{
    public class TreinoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Instrutor Instrutor { get; set; }
        
        public IList<Exercicio> Exercicios { get; set; }
    }
}