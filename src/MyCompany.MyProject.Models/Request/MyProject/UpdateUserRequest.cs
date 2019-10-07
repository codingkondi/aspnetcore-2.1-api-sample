using Newtonsoft.Json;

namespace MyCompany.MyProject.Models
{
    public class UpdateUserRequest
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
