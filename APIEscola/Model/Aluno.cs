using System.Text.Json.Serialization;

namespace APIEscola.Model
{
    public class Aluno
    {
        //[JsonIgnore]
        public long id { get; set; }

        public string nome { get; set; }
    }
}
