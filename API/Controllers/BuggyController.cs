using API.Data;
using API.Enitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;

        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetAuth()
        {
            return "I am blind";
        }
        [HttpGet("not-found")]
        public ActionResult<AppUser> GeNotFound()
        {
            var user = _context.Users.Find(-1);
            if (user == null) return NotFound();
            return user;
        }
        [HttpGet("server-error")]
        public ActionResult<AppUser> GetServerError()
        {
            var user = _context.Users.Find(-1) ?? throw new Exception("A Bad thing has happned"); 
            return user;
        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {

            return BadRequest("This is bad Request");
        }
    }
}
