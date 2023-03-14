using Newtonsoft.Json;

namespace DadJokesApp.Models
{
    public class DadJokesModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }
    }
}
