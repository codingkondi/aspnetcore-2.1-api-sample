using MyCompany.MyProject.DataBase.Models;
using MyCompany.MyProject.DataRepository.Interface;
using MyCompany.MyProject.Logic.Interface;
using MyCompany.MyProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCompany.MyProject.Logic
{
    public class ShoolLogic : IShoolLogic
    {
        #region Catch Settings from DI
        private readonly ErrorSettings _errorSettings;
        private readonly IUnitOfWorks _unitOfWorks;
        #endregion
        public ShoolLogic(ErrorSettings errorSettings, IUnitOfWorks uniofworks)
        {
            _errorSettings = errorSettings;
            _unitOfWorks = uniofworks;
        }
        public ApiResponse<UserData> CreateUser(CreateUserRequest request)
        {
            ApiResponse<UserData> response = new ApiResponse<UserData>() { };
            Users NewItem = new Users()
            {
                UserName = request.Name,
                UserAge = request.Age
            };
            if (_unitOfWorks.UsersRepository.Get(x => x.UserAge == request.Age && x.UserName == request.Name).Count() = 0)
            {
                response.SetFailedError(_errorSettings.SetError("", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Is_Duplicated));
                return response;
            }
            _unitOfWorks.UsersRepository.Insert(NewItem);
            if (!_unitOfWorks.SaveChanges)
            {
                response.SetFailedError(_errorSettings.SetError("", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Update_DB_Failed));
            }
            return response;
        }
        public ApiResponse<UserData> DeleteUser(int id)
        {
            ApiResponse<UserData> response = new ApiResponse<UserData>() { };
            Users item = _unitOfWorks.UsersRepository.Get(x => x.Id == id).FirstOrDefault();
            if (item == null)
            {
                response.SetFailedError(_errorSettings.SetError("", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Data_Is_Not_Existed));
                return response;
            }
            else
            {
                _unitOfWorks.UsersRepository.Delete(item.Id);
            }

            if (!_unitOfWorks.SaveChanges)
            {
                response.SetFailedError(_errorSettings.SetError("", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Update_DB_Failed));
            }
            return response;
        }
        public ApiResponse<UserData> GetUser(int id)
        {
            ApiResponse<UserData> response = new ApiResponse<UserData>() { };
            Users item = _unitOfWorks.UsersRepository.GetById(id);
            if (item != null)
            {
                response.Data = new UserData()
                {
                    Name = item.UserName,
                    Age = item.UserAge
                };
            }
            else
            {
                response.SetFailedError(_errorSettings.SetError("", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Data_Is_Not_Existed));

            }
            return response;
        }
        public ApiResponse<List<UserData>> GetUsers(GetUserRequest request)
        {
            ApiResponse<List<UserData>> response = new ApiResponse<List<UserData>>() { };
            response.Data = _unitOfWorks.UsersRepository.Get(
                x => (request.MaxAge > 0 ? request.MaxAge >= x.UserAge : true) &&
                     (request.MinAge > 0 ? request.MinAge <= x.UserAge : true) &&
                     (!string.IsNullOrWhiteSpace(request.Name) ? x.UserName == request.Name : true))
                .Select(x => new UserData()
                {
                    Age = x.UserAge,
                    Name = x.UserName
                }).ToList();
            return response;
        }
        public ApiResponse<UserData> UpdateUser(UpdateUserRequest request)
        {
            ApiResponse<UserData> response = new ApiResponse<UserData>() { };
            Users item = _unitOfWorks.UsersRepository.Get(x => x.Id == request.ID).FirstOrDefault();
            if (item == null)
            {
                response.SetFailedError(_errorSettings.SetError("", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Is_Duplicated));
                return response;
            }
            else
            {
                item.UserAge = request.Age;
                item.UserName = request.Name;
                _unitOfWorks.UsersRepository.Update(item);
            }

            if (!_unitOfWorks.SaveChanges)
            {
                response.SetFailedError(_errorSettings.SetError("", EnumMasterErrorCode.DataFailed, EnumSeqMessage.Update_DB_Failed));
            }

            return response;
        }
    }
}
