using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }

        [JsonIgnore]
        public IList<Matricula> Matriculas { get; set; }
    }
}