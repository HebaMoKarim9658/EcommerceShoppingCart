using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AngularProjectAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace AngularProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> UserManager;
        private IPasswordHasher<User> passwordHasher;
        public AccountController(UserManager<User> UserManager, IPasswordHasher<User> passwordHash)
        {
            this.UserManager = UserManager;
            passwordHasher = passwordHash;
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(UpdatedUser updatedUser)
        {
            User user = await UserManager.FindByIdAsync(updatedUser.id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(updatedUser.password))
                    user.PasswordHash = passwordHasher.HashPassword(user, updatedUser.password);

                if (!string.IsNullOrEmpty(updatedUser.Image))
                    user.Image = updatedUser.Image;

                if (!string.IsNullOrEmpty(updatedUser.Email))
                    user.Email = updatedUser.Email;

                if (!string.IsNullOrEmpty(updatedUser.password) || !string.IsNullOrEmpty(updatedUser.Email) || !string.IsNullOrEmpty(updatedUser.password) || !string.IsNullOrEmpty(updatedUser.Image))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return Ok();
                    else
                        return BadRequest();
                }
            }

            else
                NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegistrationModel registrationModel)
        {

            var user = new User()
            {
                UserName = registrationModel.UserName,
                Email = registrationModel.Email,
                PhoneNumber = registrationModel.PhoneNumber,
                Gender = registrationModel.Gender,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await UserManager.CreateAsync(user, registrationModel.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user, "Normal User");
                return Ok(new { UserName = user.UserName, Email = user.Email });
            }
            return BadRequest(new JsonResult("Nooo"));
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            var user = await UserManager.FindByNameAsync(loginModel.UserName);
            if(user!=null && await UserManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRole = await UserManager.GetRolesAsync(user);
                var claims = new[]
                {
                    new Claim(JwtHeaderParameterNames.Kid,user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Typ,userRole[0]),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurityKey"));
                var token= new JwtSecurityToken
                (
                   audience:"http://oec.com",
                   issuer: "http://oec.com",
                   expires:DateTime.Now.AddHours(10),
                   claims:claims,
                 
                   signingCredentials:new Microsoft.IdentityModel.Tokens.SigningCredentials(signingkey,SecurityAlgorithms.HmacSha256)
                );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration=token.ValidTo});
            }
            return BadRequest(new { message = "Username or password is incorrect" });
        }


    }
}
