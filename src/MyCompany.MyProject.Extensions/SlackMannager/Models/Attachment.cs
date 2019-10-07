using Newtonsoft.Json;
namespace MyCompany.MyProject.Extensions.SlackMannager
{
    public class Attachment
    {
        [JsonProperty("fields")]
        public Field[] Fields { get; set; }

        [JsonProperty("color")]
        public string Color = "danger";
    }
}
