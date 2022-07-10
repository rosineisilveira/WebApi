using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Treino
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ExercicioId { get; set; }
        public int InstrutorId { get; set; }
        public Instrutor Instrutor { get; set; }
        public IList<Exercicio> Exercicios { get; set; }

        [JsonIgnore]
        public IList<Matricula> Matriculas { get; set; }
    }
}