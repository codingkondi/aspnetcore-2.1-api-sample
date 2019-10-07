using Newtonsoft.Json;

namespace MyCompany.MyProject.Models
{
    public class UserData
    {
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
