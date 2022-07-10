using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Instrutor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        
        [JsonIgnore]
        public IList<Treino> Treinos { get; set; }
    }
}