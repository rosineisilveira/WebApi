using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        
         [JsonIgnore]
        public Matricula Matricula { get; set; }
        

    }
}