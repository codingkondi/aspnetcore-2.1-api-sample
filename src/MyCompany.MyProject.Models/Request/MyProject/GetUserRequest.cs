using Newtonsoft.Json;

namespace MyCompany.MyProject.Models
{
    public class GetUserRequest
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("max_age")]
        public int MaxAge { get; set; }
        [JsonProperty("min_age")]
        public int MinAge { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
