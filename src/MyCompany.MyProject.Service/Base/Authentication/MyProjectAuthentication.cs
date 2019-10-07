using Microsoft.AspNetCore.Http;
using MyCompany.MyProject.Models;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Base.Authentication
{
    public class MyProjectAuthentication 
    {
        private readonly ErrorSettings _errorSettings;
        private readonly RequestDelegate _next;
        private readonly string _erptoken;

        public MyProjectAuthentication(RequestDelegate next, ErrorSettings errorSettings, MyProjectConfiguration myProjectConfiguration)
        {
            _next = next;
            _errorSettings = errorSettings;
            _erptoken = myProjectConfiguration.TokenSettings.ErpToken;
        }

        public async Task Invoke(HttpContext context)
        {
            if (await TokenAuthentication(context))
            {
                await _next(context);
            }
        }

        private async Task<bool> TokenAuthentication(HttpContext context)
        {
            bool IsTokenSuccess = true;
            ApiResponse<object> apiResponse = new ApiResponse<object>() { };
            try
            {
                string token = context.Request.Headers["api-token"];
                if (!string.IsNullOrWhiteSpace(token))
                {
                    if (!token.Equals(_erptoken))
                    {
                        apiResponse.Status = (int)EnumMasterErrorCode.Invalid_Token;
                        apiResponse.SetInvalidTokenError(_errorSettings.SetError("", EnumMasterErrorCode.Invalid_Token, EnumSeqMessage.Token_Is_Invalid));
                        byte[] result = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(apiResponse));
                        context.Response.StatusCode = 401;
                        await context.Response.Body.WriteAsync(result, 0, result.Length);
                        return false;
                    }
                }
                else
                {
                    apiResponse.Status = (int)EnumMasterErrorCode.Invalid_Token;
                    apiResponse.SetInvalidTokenError(_errorSettings.SetError("", EnumMasterErrorCode.Invalid_Token, EnumSeqMessage.Token_Is_Required));
                    byte[] result = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(apiResponse));
                    context.Response.StatusCode = 401;
                    await context.Response.Body.WriteAsync(result, 0, result.Length);
                    return false;
                }
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = (int)EnumMasterErrorCode.Invalid_Token;
                apiResponse.SetExceptionError(_errorSettings.SetError("Validating Token:"+ex.ToString(), EnumMasterErrorCode.Invalid_Token, EnumSeqMessage.Token_Is_Invalid));
                byte[] result = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(apiResponse));
                await context.Response.Body.WriteAsync(result, 0, result.Length);
                return false;
            }
            return IsTokenSuccess;
        }
    }
}
