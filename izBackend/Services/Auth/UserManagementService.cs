using izBackend.Models;
using izBackend.Models.DB;
using izBackend.Models.Requests;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace izBackend.Services.Auth
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IzDbContext _db;
        private readonly IConfiguration _config;
        public UserManagementService( IzDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public bool IsValidUser(string userName, string password, out User user)
        {
            user = _db.Users.FirstOrDefault(x => x.Username.ToString() == userName);

            if (user == null) {

                return false;
            }
           
            if (CryptoService.VerifyPassword(password, user.Salt, user.Password))
            {
                return true;
            }
           // userID = "hahaha";
            return false;
        }

        public bool GetUserByID(string userID, out User user)
        {
            user = _db.Users.FirstOrDefault(x => x.Id.ToString() == userID);
            if (user == null) {
                return false;
            }
            return true;
        }

        public bool CreateUser(UserCreateRequest userRequest , out String error) {
            error = string.Empty;
            try
            {
                

                if (_db.Users.Any(x => x.Username.ToString() == userRequest.Username))
                {
                    error = "User Already Exists";
                    return false;
                }

                if (!ValidatePassword(userRequest.Password, out String validateError))
                {
                    error = validateError;
                    return false;
                }


                var passwordSalt = CryptoService.GenerateSalt();
                var passwordHash = CryptoService.ComputeHash(userRequest.Password, passwordSalt);

                User newUser = new User
                {
                    Username = userRequest.Username,
                    Password = BitConverter.ToString(passwordHash),
                    Salt = BitConverter.ToString(passwordSalt)
                };

                _db.Users.Add(newUser);
                _db.SaveChanges();

                return true;
            }catch(Exception e)
            {
                error = e.Message;
                return false;
            }
        }




        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            PasswordComplexity passwordComplexity = _config.GetSection("passwordComplexity").Get<PasswordComplexity>();

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Password should not be empty";
                return false;

            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{"+ passwordComplexity .MinCharacters+ @","+ passwordComplexity.MaxCharacters + @"}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (passwordComplexity.HasLowerChar && !hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (passwordComplexity.HasUpperChar && !hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than " + passwordComplexity.MinCharacters + " or greater than " + passwordComplexity.MaxCharacters + " characters";
                return false;
            }
            else if (passwordComplexity.HasNumber && !hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }
            else if (passwordComplexity.HasSymbols && !hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
