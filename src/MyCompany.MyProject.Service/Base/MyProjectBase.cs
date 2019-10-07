using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCompany.MyProject.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Service.Base
{
    public class MyProjectBase : ControllerBase
    {
        protected ILogger logger { get; set; }
        protected MyProjectBase()
        {

        }

        #region ConnectToApi
        public async Task<T> ConnectToApiAsync<T>(CallApiRequest callapirequest) where T : class
        {
            T response = null;
            try
            {
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
            }
            catch (Exception ex)
            {
                logger.LogError(Request.Path.Value + "\n" + ex.ToString());
            }
            return response;
        }

      
        #endregion
        #region Other Applications
        //The Function for copying model to new model
        public static TTarget CopyModelTo<TSource, TTarget>(TSource source) where TTarget : new()
        {
            var target = new TTarget();
            var sourceProps = typeof(TSource).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TTarget).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(target, sourceProp.GetValue(source, null), null);
                    }
                }
            }

            return target;
        }

        #endregion
    }

}
