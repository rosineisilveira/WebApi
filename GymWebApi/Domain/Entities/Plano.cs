using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Plano
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Decimal Valor { get; set; }

        [JsonIgnore]
        public IList<Matricula> Matriculas { get; set; }
    }
}