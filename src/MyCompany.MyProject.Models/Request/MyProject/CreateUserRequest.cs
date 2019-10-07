using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.MyProject.Models
{
    public class CreateUserRequest
    {
        [Required]
        [JsonProperty("age")]
        public int Age { get; set; }
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
