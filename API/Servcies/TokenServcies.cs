using API.Enitites;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Servcies
{
    public class TokenServcies : ITokenServices
    {
        private readonly IConfiguration _config;
        public TokenServcies(IConfiguration config)
        {
            _config= config;
        }
        public string CreateToken(AppUser user)
        {
            var tokenkey = _config["TokenKey"] ?? throw new Exception("Can't Access token from appsettings");
            if (tokenkey.Length < 64) throw new Exception("your token need to be longer");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.UserName)
            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =  creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken((tokenDesciptor));
            return tokenHandler.WriteToken(token);
        }
    }
}
