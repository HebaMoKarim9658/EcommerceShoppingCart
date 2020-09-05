using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularProjectAPI.Models;
using AngularProjectAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User, string, string> UserRepository;

        public UsersController(IRepository<User, string, string> _userRepository)
        {
            UserRepository = _userRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> users = UserRepository.GetAll();
            if (users.Count() > 0)
                return users.ToList();
            return NotFound();
        }

        [HttpGet("{Userid}")]
        public ActionResult<User> GetUser(string Userid)
        {
            var user = UserRepository.GetById(Userid);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("{UserName:alpha}")]
        public ActionResult<User> GetProductName(string UserName)
        {
            var User = UserRepository.GetByName(UserName);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }
        [HttpGet()]
        [Route("GetCurrentUserInfo")]
        public ActionResult<User> GetCurrentUserInfo()
        {
            var UserClaims = HttpContext.User.Claims.ToList();
            var UserID = UserClaims[4].Value;

            var User = UserRepository.GetById(UserID);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }
      
        [HttpPut("{id}")]
        public IActionResult PutUser(string id, User user)
        {
            Console.WriteLine($"Before First Id {id} and User id is : {user.Id}");
            if (id != user.Id)
            {
                return BadRequest();
            }
            if (!UserExists(id))
            {
                return NotFound();
            }
            UserRepository.Update(user);
            return NoContent();
        }


        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            UserRepository.Add(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{Did}")]
        public ActionResult<User> DeleteUser(string Did)
        {
            var user = UserRepository.GetById(Did);
            if (user == null)
            {
                return NotFound();
            }

            UserRepository.Delete(user);
            return user;
        }

        private bool UserExists(string id)
        {
            if (UserRepository.GetById(id) == null)
                return false;
            return true;
        }
    }
}