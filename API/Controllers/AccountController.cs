using API.Data;
using API.DTOs;
using API.Enitites;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenServices _tokenServcies;
        public AccountController(DataContext context,ITokenServices tokenServcies)
        {
            _context = context;
            _tokenServcies = tokenServcies;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerdto)
        {
            if (await UserExists(registerdto.Username)) return BadRequest("User Allready taken!");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerdto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerdto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenServcies.CreateToken(user)
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid UserName");
            using var hmac =  new HMACSHA512(user.PasswordSalt);
            var computedhmac =  hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0; i<computedhmac.Length; i++)
            {
                if (computedhmac[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");

            }
            return new UserDto { 
                UserName = user.UserName, Token = _tokenServcies.CreateToken(user) };
        }
        private async Task<bool> UserExists(string un)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == un.ToLower());
        }

    }
}
