using Newtonsoft.Json;

namespace MyCompany.MyProject.Models
{
    public class Error
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

}
