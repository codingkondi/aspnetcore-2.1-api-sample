using MyCompany.MyProject.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyCompany.MyProject.Models
{
    public class ApiResponse<T> : IApiResponse<T> where T : class
    {

        private int status = 200;
        [JsonProperty("status")]
        public int Status { get => status; set { if (value >= 0) status = value; } }
        public T Data { get; set; }
        [JsonProperty("error")]
        public List<Error> Error { get; set; }

        public void SetFailedError(Error error)
        {
            Status = (int)EnumMasterErrorCode.DataFailed;
            Error = new List<Error>() { error };
        }

        public void SetExceptionError(Error error)
        {
            Status = (int)EnumMasterErrorCode.Internal_System_Error;
            Error = new List<Error>() { error };
        }
        public void SetInvalidTokenError(Error error)
        {
           
            Status = (int)EnumMasterErrorCode.Invalid_Token;
            Error = new List<Error>() { error };
        }
    }


}
