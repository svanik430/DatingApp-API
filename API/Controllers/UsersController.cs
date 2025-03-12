using API.Data;
using API.DTOs;
using API.Enitites;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController:BaseApiController
    {
        public readonly IUserRepository _userRepository;
        //public readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
           
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembesrAsync();
           
            return Ok(users);
        }

        //[HttpGet("{users/id}")]
        //public async Task<ActionResult<MemberDto>> GetUser(int id)
        //{
        //    var user = await _userRepository.GetUsersByIdAsync(id);
        //    // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(user);

        //    //if (user == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //return Ok(usersToReturn);

        //    if (user == null) return NotFound();
        //    return _mapper.Map<MemberDto>(user);
        //}
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
            // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(user);

            //if (user == null)
            //{
            //    return NotFound();
            //}
            //return Ok(usersToReturn);

            if (user == null) return NotFound();
            return user;
        }
    }
}
