using MyCompany.MyProject.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Logic.APIConnection
{
    public class APIConnection
    {
        public APIConnection()
        {

        }

        public async Task<T> ConnectToApiAsync<T>(CallApiRequest callapirequest) where T : class
        {
            T response = null;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                // Content-Type 用於宣告遞送給對方的文件型態
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                var encodedContent = new FormUrlEncodedContent(callapirequest.Params);

                //Decide what method the api use
                switch (callapirequest.ApiMethod)
                {
                    case EnumHttpMethod.POST:
                        httpResponse = await client.PostAsync(callapirequest.Url, encodedContent);
                        break;
                    case EnumHttpMethod.GET:
                        httpResponse = await client.GetAsync(callapirequest.Url);
                        break;
                }

                response = await httpResponse.Content.ReadAsAsync<T>();
            }
            return response;
        }
    }


}
