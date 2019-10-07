using MyCompany.MyProject.Models;
using System.Collections.Generic;

namespace MyCompany.MyProject.Logic.Interface
{
    public interface IShoolLogic
    {
        ApiResponse<UserData> CreateUser(CreateUserRequest request);
        ApiResponse<UserData> DeleteUser(int id);
        ApiResponse<UserData> UpdateUser(UpdateUserRequest request);
        ApiResponse<UserData> GetUser(int id);
        ApiResponse<List<UserData>> GetUsers(GetUserRequest request);


    }
}
