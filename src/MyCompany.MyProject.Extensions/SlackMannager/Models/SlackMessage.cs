using Newtonsoft.Json;
namespace MyCompany.MyProject.Extensions.SlackMannager
{
    public class SlackMessage
    {
        [JsonProperty("attachments")]
        public Attachment[] Attachment { get; set; }
        [JsonProperty("username")]
        public string ServiceType { get; set; }
    }

}
