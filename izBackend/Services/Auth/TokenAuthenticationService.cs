using izBackend.Models;
using izBackend.Models.Common.Auth;
using izBackend.Models.DB;
using izBackend.Models.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace izBackend.Services.Auth
{
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IzDbContext _db;
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;
        private readonly IConfiguration _config;



        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement, IConfiguration config , IzDbContext db)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
            _config = config;
            _db = db;
        }
        public bool IsAuthenticated(TokenRequest request, out AuthResponse res )
        {

            res = new AuthResponse();
            if (!_userManagementService.IsValidUser(request.Username, request.Password, out User user)) return false;
            return GenerateTokensFromUser(out res, user);

        }

    
        public string GetUserID(ClaimsPrincipal User) {


            TokenManagement token = _config.GetSection("tokenManagement").Get<TokenManagement>();
            if (User.Claims.First(i => i.Type == "aud").Value == token.FirebaseProject)
            {
                return  User.Claims.First(i => i.Type == "user_id").Value;
            }
            return  User.Claims.First(i => i.Type == "user_id").Value;
        }


        public bool RenewToken(RefreshTokenRequest request, out AuthResponse res)
        {

            string userId = string.Empty;
            string token = string.Empty;
            string refreshToken = string.Empty;
            res = new AuthResponse();


            if(_db.RefreshTokens.Any(x => x.User.Id.ToString() == request.UserID && x.Token == request.RefreshToken))
            {
                User user = _db.Users.FirstOrDefault(x => x.Id.ToString().ToLower() == request.UserID.ToLower());
                return GenerateTokensFromUser(out res, user);
            }

            return false;

        }


        private bool GenerateTokensFromUser(out AuthResponse res, User user)
        {
            Claim[] claims = new[]
                        {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.AuthenticationMethod , nameof(user.Source) ),
                new Claim("user_id", user.Id.ToString()),
                new Claim("aud",  nameof(user.Source))
            };

            string token = GenerateToken(claims);
            string refreshToken = GenerateRefreshTokenValue();

            res = new AuthResponse(token, refreshToken, DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration), user.Id.ToString());
            SaveGeneratedRefreshToken(res);

            return true;
        }


        private string GenerateRefreshTokenValue()
        {
            Guid g = Guid.NewGuid();
            return g.ToString();
        }
         
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private void SaveGeneratedRefreshToken(AuthResponse res)
        {
            User currentUser = new User();
            if (_userManagementService.GetUserByID(res.UserID, out currentUser))
            {
                RefreshToken r = new RefreshToken
                {
                    ExpiresUtc = res.Expiery,
                    IssuedUtc = DateTime.Now,
                    OriginalToken = res.OriginalToken,
                    Token = res.RefreshToken,
                    User = currentUser
                };
                _db.RefreshTokens.RemoveRange(_db.RefreshTokens.Where(x => x.User.Id == currentUser.Id));
                _db.RefreshTokens.Add(r);
                _db.SaveChanges();
            }

        }


    }
}
