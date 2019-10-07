using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.MyProject.Models
{
    public class DeleteUserRequest
    {
        [Required]
        [JsonProperty("id")]
        public int ID { get; set; }
    }
}
