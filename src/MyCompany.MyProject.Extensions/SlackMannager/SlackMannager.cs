using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Extensions.SlackMannager
{
    public class SlackMannager
    {
        private readonly Uri _uri;
        private readonly string _servicetype;
        private SlackMessage _message;

        public SlackMannager(string webhooks_url, string servicetype)
        {
            _uri = new Uri(webhooks_url);
            _servicetype = servicetype;
        }

        public void SetMessage(Field[] fields,string color)
        {
            _message = new SlackMessage()
            {
                ServiceType = _servicetype, // slack app name,using different service type as it's name
                Attachment = new Attachment[1] {
                  
                        new Attachment() {Color=ColorSettings(color) }
                    }
            };
            _message.Attachment[0].Fields = fields;
        }

        private string ColorSettings(string inputcolor)
        {
            //If input color is invaild just return "good"(green color)

            if (inputcolor == "danger" || inputcolor == "warning" || inputcolor == "good")
                return inputcolor;

            if (inputcolor.Substring(0, 1) == "#" && inputcolor.Length == 7)
            {
                if (Regex.Match(inputcolor, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success)
                    return inputcolor;
                else
                    return "good";
            }
            else
                return "good";
        }

        public async Task<bool> SendMessage()
        {
            bool IsSuccessed = true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage();
                    // Content-Type 用於宣告遞送給對方的文件型態
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    httpResponse = await client.PostAsJsonAsync(_uri, _message);
                    if (!httpResponse.IsSuccessStatusCode)
                        IsSuccessed = false;
                }
            }
            catch (Exception)
            {
                IsSuccessed = false;
            }
            return IsSuccessed;
        }
    }
}