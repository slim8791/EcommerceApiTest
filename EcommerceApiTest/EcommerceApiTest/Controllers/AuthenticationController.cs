using EcommerceApiTest.Authentication;
using EcommerceApiTest.Data;
using JwtAuthntication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EcommerceApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly EcommerceContext _context;

        public AuthenticationController(UserManager<IdentityUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        IConfiguration configuration,
                                        EcommerceContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRole = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var item in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, item));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }
            return Unauthorized();

        }

        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(3),
                claims: claims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var userNameExists = await _userManager.FindByNameAsync(registerModel.UserName);
            var userEmailExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userNameExists != null || userEmailExists != null)
                return StatusCode(StatusCodes.Status406NotAcceptable, new ResponseModel { Status = "Error", Message = "User already exists" });

            IdentityUser user = new IdentityUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                PhoneNumber = registerModel.Phone,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByEmailAsync(registerModel.Email);
                User u = new User {
                    AspNetUserId = newUser.Id,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName
                };

                _context.User.Add(u);
                await _context.SaveChangesAsync();

                await AttachUserToRole("User", newUser);

                return StatusCode(StatusCodes.Status201Created, new ResponseModel { Status = "Success", Message = "User created" });
            }


            return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Status = "Error", Message = "an error has occured" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("addrole")]
        public async Task<IActionResult> AddRole([FromBody] RoleModel roleModel)
        {
            if (!await _roleManager.RoleExistsAsync(roleModel.RoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleModel.RoleName));
                return StatusCode(StatusCodes.Status201Created, new ResponseModel { Status = "Success", Message = "Role created" });

            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Status = "Error", Message = "Role already exists" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = _roleManager.Roles.ToList();
            return Ok(result);
        }

        protected async Task AttachUserToRole(string role, IdentityUser user)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        [HttpPost]
        [Route("adminregister")]
        public async Task<IActionResult> AdminRegister([FromBody] RegisterModel registerModel)
        {
            var userNameExists = await _userManager.FindByNameAsync(registerModel.UserName);
            var userEmailExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userNameExists != null || userEmailExists != null)
                return StatusCode(StatusCodes.Status406NotAcceptable, new ResponseModel { Status = "Error", Message = "User already exists" });

            IdentityUser user = new IdentityUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Status = "Error", Message = "an error has occured" });

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            await _userManager.AddToRoleAsync(user, "Admin");
            return StatusCode(StatusCodes.Status201Created, new ResponseModel { Status = "Success", Message = "User created" });
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var userNameExists = await _userManager.FindByNameAsync(model.UserName);
            if (userNameExists != null)
            {
                var passwordTken = await _userManager.GeneratePasswordResetTokenAsync(userNameExists);
                var result = await _userManager.ResetPasswordAsync(userNameExists, passwordTken, model.Password);

                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Status = "Error", Message = "an error has occured" });
                return StatusCode(StatusCodes.Status205ResetContent, new ResponseModel { Status = "Success", Message = "password was resetted successfuly" });

            }
            return NotFound("User not found");
        }

        [HttpPut]
        [Route("user")]
        public async Task<IActionResult> UpdateUser([FromBody] RegisterModel registerModel)
        {
            var user = await _userManager.FindByNameAsync(registerModel.UserName);

            var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, registerModel.Email);

            await _userManager.ChangeEmailAsync(user, registerModel.Email, emailToken);

            var phoneToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, registerModel.Phone);

            await _userManager.ChangePhoneNumberAsync(user, registerModel.Phone, phoneToken);

            var us = _context.User.Where(u => u.AspNetUserId.Equals(user.Id)).FirstOrDefault();

            us.FirstName = registerModel.FirstName;
            us.LastName = registerModel.LastName;

            _context.Entry(us).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("user/{userName}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            await _userManager.DeleteAsync(user);

            var us = _context.User.Where(u => u.AspNetUserId.Equals(user.Id)).FirstOrDefault();

            _context.Remove(us);

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var userNameExists = await _userManager.FindByNameAsync(model.UserName);
            if (userNameExists != null)
            {
                var result = await _userManager.ChangePasswordAsync(userNameExists, model.Password, model.NewPassword);

                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Status = "Error", Message = "an error has occured" });
                return StatusCode(StatusCodes.Status205ResetContent, new ResponseModel { Status = "Success", Message = "password was resetted successfuly" });

            }
            return NotFound("User not found");
        }

    }
}
