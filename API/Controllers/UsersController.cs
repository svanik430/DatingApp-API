using API.Data;
using API.Enitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   
    public class UsersController:BaseApiController
    {
        public readonly DataContext _dataContext;
        public UsersController(DataContext dataContext)
        {
            _dataContext= dataContext;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _dataContext.Users.ToListAsync();
            if(users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
