using izBackend.Models.DB;
using izBackend.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Services.Auth
{
    public interface IUserManagementService
    {
        bool IsValidUser(string username, string password, out User userID);
        bool CreateUser(UserCreateRequest userRequest, out String error);
        bool GetUserByID(string userID, out User user);
    }
}
