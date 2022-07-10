using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Exercicio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Repeticao { get; set; }
        public int Series { get; set; }

        [JsonIgnore]
        public IList<Treino> Treinos { get; set; }
    }
}