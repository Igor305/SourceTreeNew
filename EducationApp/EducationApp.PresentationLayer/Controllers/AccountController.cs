using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EducationApp.DataAccessLayer.Entities;
using System;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using EducationApp.BusinessLogicLayer.Models.Account;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }
        public ActionResult<IEnumerable<string>> GetAuth()                                                                                                       //GetAuth
        {
            var Id = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var passHash = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Hash);
            var role = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (_httpContextAccessor.HttpContext.Response.StatusCode == 401)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = 200;
                var refresh = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Refresh");
                return new string[] { "Error 401" };
            }
            return new string[] { Id?.Value, email?.Value, passHash?.Value, role?.Value };
        }
        public async Task<ActionResult<string>> PostAuth(LoginModel login, IJwtPrivateKey jwtPrivateKey, IJwtRefresh jwtRefresh)                                //PostAuth
        {
            // Validate email
            User user = new User();
            var userX = await _userManager.FindByEmailAsync(login.Email);
            if (userX == null)
            {
                return "Вы ввели не правильный email. Возможно вы еще не зарегистрировались. Бегите, станьте нашим 1000-м посетителем!";
            }
            var confirmpass = await _userManager.CheckPasswordAsync(userX, login.Password);
            if (!confirmpass)
            {
                return "Вы ввели не правильный пароль.";
            }
            await _userManager.AddToRoleAsync(userX, "User");
            // Token.    
            var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier, userX.Id.ToString()),
            new Claim(ClaimTypes.Email,userX.Email),
            new Claim(ClaimTypes.Hash, userX.PasswordHash),
            new Claim(ClaimTypes.Role, "User"),
            };
            // JWT.
            var token = new JwtSecurityToken(
                issuer: "MyJwt",
                audience: "TheBestClient",
                claims: claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: new SigningCredentials(
                        jwtPrivateKey.GetKey(),
                        jwtPrivateKey.SigningAlgorithm)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var claimsref = new List<Claim>()
            {
            new Claim("Refresh", userX.Email)
            };
            // JWT.
            var refreshtoken = new JwtSecurityToken(
                issuer: "MyJwt",
                audience: "TheBestClient",
                claims: claimsref,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: new SigningCredentials(
                        jwtRefresh.GetKey(),
                        jwtRefresh.SigningAlgorithm)
            );
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshtoken);
            return "RefreshToken =      " + refreshToken + "    AccessToken  =     " + jwtToken;
        }
        public async Task<ActionResult<string>> Register(RegisterModel reg)                                                                                     //Register
        {
            User user = new User { Email = reg.Email, UserName = reg.Email };
            var result = await _userManager.CreateAsync(user, reg.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var regurl = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).Action(
                    "ConfirmEmail",
                    "Account",
                    new { userId = user.Id, code = code },
                    protocol: _httpContextAccessor.HttpContext.Request.Scheme);
                string subject = "Подтверждение регистрации";
                string message = $"Подтвердите регистрацию, перейдя по ссылке: <a href={regurl}>Confirm email</a>";
                await _emailService.SendEmail(reg.Email, subject, message);
                await _signInManager.SignInAsync(user, false);
                return "Confirm account from email";
            }
            return result.ToString();
        }
        public async Task<ActionResult<string>> ForgotPassword(ForgotPassword forgotPassword)                                                                                 //ForgotPassword
        {
            var user = await _userManager.FindByNameAsync(forgotPassword.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return "ForgotPasswordConfirmation";
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).Action(
                "ResetPassword",
                "Account",
            new { userEmail = user.Email, code = code },
            protocol: _httpContextAccessor.HttpContext.Request.Scheme);
            string subject = "Изменение пароля ";
            string message = $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>Нажми на меня</a>";
            await _emailService.SendEmail(forgotPassword.Email, subject, message);
            return "Confirm reset password from email";
        }
        public async Task<ActionResult<string>> ConfirmEmail(string userId, string code)                                                                           //ConfirmEmail
        {
            if (userId == null || code == null)
            {
                return "Error, userId or code null";
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "Error, not user";
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return user.ToString();
            }
            return "Error, not Succeeded";
        }
        public async Task<ActionResult<string>> ResetPassword(ResetPasswordModel reset)                                                                             //ResetPassword
        {
            if (reset.Code == null || reset.Email == null)
            {
                return "Error";
            }
            var user = await _userManager.FindByNameAsync(reset.Email);
            if (user == null)
            {
                return "ResetPasswordConfirmation";
            }
            await _userManager.CheckPasswordAsync(user, reset.Password);
            var result = await _userManager.ResetPasswordAsync(user, reset.Code, reset.Password);
            if (result.Succeeded)
            {
                return user.ToString();
            }
            return "Error, not Succeeded";
        }
        public async Task<ActionResult<string>> LogOut(LogOutModel logOutModel)                                                                                              //LogOut
        {
            await _signInManager.SignOutAsync();
            logOutModel.Email = logOutModel.Password = null;
            return "LogOut";
        }
        public async Task<ActionResult<string>> RefreshToken(RefreshTokenModel refreshTokenModel, IJwtPrivateKey jwtPrivateKey, IJwtRefresh jwtRefresh)             //RefreshToken
        {
            var jwtEncodedString = refreshTokenModel.tokenString.Substring(7);
            User user = new User();
            var dtoken = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            string decodingtoken = dtoken.Claims.First(c => c.Type == "Refresh").Value;
            var UserX = await _userManager.FindByEmailAsync(decodingtoken);
            if (UserX == null)
            {
                return "Тебя нету в бд";
            }
            // Token.    
            var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier, UserX.Id.ToString()),
            new Claim(ClaimTypes.Email,UserX.Email),
            new Claim(ClaimTypes.Hash, UserX.PasswordHash),
            new Claim(ClaimTypes.Role, "User"),
            };
            // JWT.
            var token = new JwtSecurityToken(
                issuer: "MyJwt",
                audience: "TheBestClient",
                claims: claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: new SigningCredentials(
                        jwtPrivateKey.GetKey(),
                        jwtPrivateKey.SigningAlgorithm)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var claimsref = new List<Claim>()
            {
            new Claim("Refresh", UserX.Email)
            };
            // JWT.
            var refreshtoken = new JwtSecurityToken(
                issuer: "MyJwt",
                audience: "TheBestClient",
                claims: claimsref,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: new SigningCredentials(
                        jwtRefresh.GetKey(),
                        jwtRefresh.SigningAlgorithm)
            );
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshtoken);
            return "RefreshToken =      " + refreshToken + "    AccessToken  =     " + jwtToken;
        }
        public ICollection<Role> GetAllRoleUsers()                                                                                                                          //GetAllRoleUsers
        {
            var all = _roleManager.Roles.ToList();
            return all;
        }
        public async Task<ActionResult<string>> CreateRoleUsers(CreateRoleModel createRoleModel)                                                                            //CreateRoleUsers
        {
            var role = new Role();
            bool x = await _roleManager.RoleExistsAsync("Admin");
            if (!x)
            {
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);

                User user = new User();
                user.UserName = "God";
                user.Email = "igortalavuria@gmail.com";
                string userPWD = "Igor12345";
                IdentityResult Admin = await _userManager.CreateAsync(user, userPWD);

                if (Admin.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
            role.Name = createRoleModel.NameRole;
            await _roleManager.CreateAsync(role);
            return $"Create role Admin, {createRoleModel.NameRole}";
        }
        public async Task<ActionResult<string>> DeleteRoleUsers(DeleteRoleModel deleteRoleModel)                                                                              //DeleteRoleUsers
        {
            var delrole = await _roleManager.FindByNameAsync(deleteRoleModel.NameRole);
            await _roleManager.DeleteAsync(delrole);
            return $"Delete role {deleteRoleModel.NameRole}";
        }
        public async Task<ActionResult<string>> ChangeRoleUser(ChangeRoleUserModel changeRoleUserModel)                                                                        //ChangeRoleUser
        {
            var findUser = _userManager.Users.FirstOrDefault(x => x.Email == changeRoleUserModel.Email);
            var findRole = _roleManager.Roles.FirstOrDefault(x => x.Name == changeRoleUserModel.NameRole);
            UserInRole userInRole = new UserInRole
            {
                UserId = findUser.Id,
                RoleId = findRole.Id
            };
            return $"{findUser.Id} - {findRole.Id}";
        }
    }
}
