using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Base;
using MyCompany.MyProject.Logic.Interface;
using MyCompany.MyProject.Models;
using MyCompany.MyProject.Service.Base;
using System;
using System.Collections.Generic;

namespace MyCompany.MyProject.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SchoolController : MyProjectBase
    {
        private readonly IShoolLogic _logic;
        private readonly ErrorSettings _errorSettings;
        public SchoolController(ErrorSettings errorSettings, IShoolLogic logic)
        {
            _logic = logic;
            _errorSettings = errorSettings;
        }

        // Post: MyProject/users
        [Route("users")]
        [HttpPost]
        [MiddlewareFilter(typeof(MyProjectAuthenFilterMiddleware))]
        public ApiResponse<UserData> CreateUser([FromBody]ApiRequest<CreateUserRequest> request)
        {
            #region property   
            #endregion
            ApiResponse<UserData> response = new ApiResponse<UserData>() { };
            try
            {
                response = _logic.CreateUser(request.Data);
            }
            catch (Exception ex)
            {
                response.SetExceptionError(_errorSettings.SetExceptionError(Request.Path.Value, ex.ToString()));
            }
            return response;
        }
        // Delete: MyProject/users
        [Route("users/{delete_id}")]
        [HttpDelete("{delete_id}")]
        [MiddlewareFilter(typeof(MyProjectAuthenFilterMiddleware))]
        public ApiResponse<UserData> DeleteUser(int delete_id)
        {
            #region property
            #endregion
            ApiResponse<UserData> response = new ApiResponse<UserData>() { };
            if (delete_id <= 0)
                response.SetFailedError(_errorSettings.SetError("ID", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Is_Invalid));

            try
            {
                response = _logic.DeleteUser(delete_id);
            }
            catch (Exception ex)
            {
                response.SetExceptionError(_errorSettings.SetExceptionError(Request.Method+Request.Path.Value, ex.ToString()));
            }
            return response;
        }

        // Update: MyProject/users
        [Route("users")]
        [HttpPatch]
        [MiddlewareFilter(typeof(MyProjectAuthenFilterMiddleware))]
        public ApiResponse<UserData> UpdateUser([FromBody]ApiRequest<UpdateUserRequest> request)
        {
            #region property   
            #endregion
            ApiResponse<UserData> response = new ApiResponse<UserData>() { };
            try
            {
                response = _logic.UpdateUser(request.Data);
            }
            catch (Exception ex)
            {
                response.SetExceptionError(_errorSettings.SetExceptionError(Request.Path.Value, ex.ToString()));
            }
            return response;
        }

        // Get: MyProject/users
        [Route("users/{id}")]
        [HttpGet("{id}")]
        [MiddlewareFilter(typeof(MyProjectAuthenFilterMiddleware))]
        public ApiResponse<UserData> GetUser(int id)
        {
            #region property   
            #endregion
            ApiResponse<UserData> response = new ApiResponse<UserData>() { };
            try
            {
                response = _logic.GetUser(id);
            }
            catch (Exception ex)
            {
                response.SetExceptionError(_errorSettings.SetExceptionError(Request.Path.Value, ex.ToString()));
            }
            return response;
        }

        // Get: MyProject/users
        [Route("users")]
        [HttpGet]
        [MiddlewareFilter(typeof(MyProjectAuthenFilterMiddleware))]
        public ApiResponse<List<UserData>> GetUsers([FromBody]ApiRequest<GetUserRequest> request)
        {
            #region property   
            #endregion
            ApiResponse<List<UserData>> response = new ApiResponse<List<UserData>>() { };
            if (request.Data.MaxAge > 0 && request.Data.MinAge > 0 && request.Data.MinAge > request.Data.MaxAge)
            {
                response.SetFailedError(_errorSettings.SetError("Age range", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Is_Invalid));
                return response;
            }
            try
            {
                response = _logic.GetUsers(request.Data);
            }
            catch (Exception ex)
            {
                response.SetExceptionError(_errorSettings.SetExceptionError(Request.Path.Value, ex.ToString()));
            }
            return response;
        }

    }
}
