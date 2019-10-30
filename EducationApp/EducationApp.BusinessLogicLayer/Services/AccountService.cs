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
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;
using AutoMapper;

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
        private readonly IMapper _mapper;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _mapper = mapper;
        }
        public AuthAccountResponseModel GetAuth()                                                                                                       //GetAuth
        {
            AuthAccountResponseModel authAccountResponseModel = new AuthAccountResponseModel();
            Claim id = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            Claim email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            Claim passHash = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Hash);
            Claim role = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (_httpContextAccessor.HttpContext.Response.StatusCode == 401)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = 200;
                Claim refresh = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Refresh");
                authAccountResponseModel.Messege = "Error 401";
                authAccountResponseModel.Status = false;
                authAccountResponseModel.Error.Add("Unauthorized");
                return authAccountResponseModel;
            }
            Guid Id = Guid.Parse(id.Value);
            authAccountResponseModel.Id = Id;
            authAccountResponseModel.Email = email.Value;
            authAccountResponseModel.PassHash = passHash.Value;
            authAccountResponseModel.Role = role.Value;
            authAccountResponseModel.Messege = "Successfully";
            authAccountResponseModel.Status = true;
            return authAccountResponseModel;
        }
        public async Task<LoginAccountResponseModel> PostAuth(LoginModel login, IJwtPrivateKey jwtPrivateKey, IJwtRefresh jwtRefresh)                                //PostAuth
        {
            LoginAccountResponseModel loginAccountResponseModel = new LoginAccountResponseModel();
            // Validate email
            User userX = await _userManager.FindByEmailAsync(login.Email);
            if (userX == null)
            {
                loginAccountResponseModel.Messege = "Error";
                loginAccountResponseModel.Status = false;
                loginAccountResponseModel.Error.Add("The email address you entered is not valid. Perhaps you have not registered yet. Run, become our 1000th visitor!");
                return loginAccountResponseModel;
            }
            bool confirmpass = await _userManager.CheckPasswordAsync(userX, login.Password);
            if (!confirmpass)
            {
                loginAccountResponseModel.Messege = "Error";
                loginAccountResponseModel.Status = false;
                loginAccountResponseModel.Error.Add("You entered the wrong password");
                return loginAccountResponseModel;
            }
            await _userManager.AddToRoleAsync(userX, "User");
            // Token.    
            List<Claim> claims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier, userX.Id.ToString()),
            new Claim(ClaimTypes.Email,userX.Email),
            new Claim(ClaimTypes.Hash, userX.PasswordHash),
            new Claim(ClaimTypes.Role, "User"),
            };
            // JWT.
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "MyJwt",
                audience: "TheBestClient",
                claims: claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: new SigningCredentials(
                        jwtPrivateKey.GetKey(),
                        jwtPrivateKey.SigningAlgorithm)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            List<Claim> claimsref = new List<Claim>()
            {
            new Claim("Refresh", userX.Email)
            };
            // JWT.
            JwtSecurityToken refreshtoken = new JwtSecurityToken(
                issuer: "MyJwt",
                audience: "TheBestClient",
                claims: claimsref,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: new SigningCredentials(
                        jwtRefresh.GetKey(),
                        jwtRefresh.SigningAlgorithm)
            );
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshtoken);
            loginAccountResponseModel.AccessToken = jwtToken;
            loginAccountResponseModel.RefreshToken = refreshToken;
            loginAccountResponseModel.Messege = "Successfully";
            loginAccountResponseModel.Status = true;
            return loginAccountResponseModel;
        }
        public async Task<RegisterAccountResponseModel> Register(RegisterModel reg)                                                                                     //Register
        {
            RegisterAccountResponseModel registerAccountResponseModel = new RegisterAccountResponseModel();
            User user = new User { Email = reg.Email, UserName = reg.Email };
            IdentityResult result = await _userManager.CreateAsync(user, reg.Password);
            if (result.Succeeded)
            {
                string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string regurl = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).Action(
                    "ConfirmEmail",
                    "Account",
                    new { userId = user.Id, code = code },
                    protocol: _httpContextAccessor.HttpContext.Request.Scheme);
                string subject = "Confirm register";
                string message = $"Confirm registration by clicking on the link: <a href={regurl}>Confirm email</a>";
                await _emailService.SendEmail(reg.Email, subject, message);
                registerAccountResponseModel.Messege = $"Confirm account from email {reg.Email}";
                registerAccountResponseModel.Status = true;
                return registerAccountResponseModel;
            }
            registerAccountResponseModel.Messege = "Error";
            registerAccountResponseModel.Status = false;
            registerAccountResponseModel.Error.Add("You entered incorrect data");
            return registerAccountResponseModel;
        }
        public async Task<ForgotPasswordResponseModel> ForgotPassword(ForgotPassword forgotPassword)                                                                                 //ForgotPassword
        {
            ForgotPasswordResponseModel forgotPasswordResponseModel = new ForgotPasswordResponseModel();
            User user = await _userManager.FindByNameAsync(forgotPassword.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                forgotPasswordResponseModel.Messege = "Error";
                forgotPasswordResponseModel.Status = false;
                forgotPasswordResponseModel.Error.Add("Not correct Email");
                return forgotPasswordResponseModel;
            }
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            string callbackUrl = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).Action(
                "ResetPassword",
                "Account",
            new { userEmail = user.Email, code = code },
            protocol: _httpContextAccessor.HttpContext.Request.Scheme);
            string subject = "Change password";
            string message = $"To reset your password, follow the link: <a href='{callbackUrl}'>Change password</a>";
            await _emailService.SendEmail(forgotPassword.Email, subject, message);
            forgotPasswordResponseModel.Messege = $"Confirm account from email {forgotPassword.Email}";
            forgotPasswordResponseModel.Status = true;
            return forgotPasswordResponseModel;
        }
        public async Task<ConfirmEmailAccountResponseModel> ConfirmEmail(string userId, string code)                                                                           //ConfirmEmail
        {
            ConfirmEmailAccountResponseModel confirmEmailAccountResponseModel = new ConfirmEmailAccountResponseModel();
            if (userId == null || code == null)
            {
                confirmEmailAccountResponseModel.Messege = "Error";
                confirmEmailAccountResponseModel.Status = false;
                confirmEmailAccountResponseModel.Error.Add("UserId or code null");
            }
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                confirmEmailAccountResponseModel.Messege = "Error";
                confirmEmailAccountResponseModel.Status = false;
                confirmEmailAccountResponseModel.Error.Add("User is not found");
            }
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                confirmEmailAccountResponseModel.Messege = "Successfully";
                confirmEmailAccountResponseModel.Status = true;
            }
            return confirmEmailAccountResponseModel;
        }
        public async Task<ResetPasswordAccountResponseModel> ResetPassword(ResetPasswordModel reset)                                                                             //ResetPassword
        {
            ResetPasswordAccountResponseModel resetPasswordAccountResponseModel = new ResetPasswordAccountResponseModel();
            if (reset.Code == null || reset.Email == null)
            {
                resetPasswordAccountResponseModel.Messege = "Error";
                resetPasswordAccountResponseModel.Status = false;
                resetPasswordAccountResponseModel.Error.Add("UserId or code null");
            }
            User user = await _userManager.FindByNameAsync(reset.Email);
            if (user == null)
            {
                resetPasswordAccountResponseModel.Messege = "Error";
                resetPasswordAccountResponseModel.Status = false;
                resetPasswordAccountResponseModel.Error.Add("User is not found");
            }
            await _userManager.CheckPasswordAsync(user, reset.Password);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, reset.Code, reset.Password);
            if (result.Succeeded)
            {
                resetPasswordAccountResponseModel.Messege = "Successfully";
                resetPasswordAccountResponseModel.Status = true;
            }
            return resetPasswordAccountResponseModel;
        }

        public async Task<RefreshTokenAccountResponseModel> RefreshToken(RefreshTokenModel refreshTokenModel, IJwtPrivateKey jwtPrivateKey, IJwtRefresh jwtRefresh)             //RefreshToken
        {
            RefreshTokenAccountResponseModel refreshTokenAccountResponseModel = new RefreshTokenAccountResponseModel();
            string jwtEncodedString = refreshTokenModel.tokenString.Substring(7);
            User user = new User();
            JwtSecurityToken dtoken = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            string decodingtoken = dtoken.Claims.First(c => c.Type == "Refresh").Value;
            User UserX = await _userManager.FindByEmailAsync(decodingtoken);
            if (UserX == null)
            {
                refreshTokenAccountResponseModel.Messege = "Error";
                refreshTokenAccountResponseModel.Status = false;
                refreshTokenAccountResponseModel.Error.Add("User is not found");
                return refreshTokenAccountResponseModel;
            }
            // Token.    
            List<Claim> claims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier, UserX.Id.ToString()),
            new Claim(ClaimTypes.Email,UserX.Email),
            new Claim(ClaimTypes.Hash, UserX.PasswordHash),
            new Claim(ClaimTypes.Role, "User"),
            };
            // JWT.
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "MyJwt",
                audience: "TheBestClient",
                claims: claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: new SigningCredentials(
                        jwtPrivateKey.GetKey(),
                        jwtPrivateKey.SigningAlgorithm)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            List<Claim> claimsref = new List<Claim>()
            {
            new Claim("Refresh", UserX.Email)
            };
            // JWT.
            JwtSecurityToken refreshtoken = new JwtSecurityToken(
                issuer: "MyJwt",
                audience: "TheBestClient",
                claims: claimsref,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: new SigningCredentials(
                        jwtRefresh.GetKey(),
                        jwtRefresh.SigningAlgorithm)
            );
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshtoken);
            refreshTokenAccountResponseModel.AccessToken = jwtToken;
            refreshTokenAccountResponseModel.RefreshToken = refreshToken;
            refreshTokenAccountResponseModel.Messege = "Successfully";
            refreshTokenAccountResponseModel.Status = true;
            return refreshTokenAccountResponseModel;
        }
        public RoleAccountResponseModel GetAllRoleUsers()                                                                                                                          //GetAllRoleUsers
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            List<Role> roles = _roleManager.Roles.ToList();
            List<RoleAccountModel> roleAccountModels = _mapper.Map<List<Role>, List<RoleAccountModel>>(roles);
            roleAccountResponseModel.roleAccountModels = roleAccountModels;
            roleAccountResponseModel.Messege = "Successfully";
            roleAccountResponseModel.Status = true;
            return roleAccountResponseModel;
        }
        public async Task<RoleAccountResponseModel> CreateRoleUser(CreateRoleModel createRoleModel)                                                                            //CreateRoleUser
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            if (createRoleModel.Name == null)
            {
                roleAccountResponseModel.Messege = "Error";
                roleAccountResponseModel.Status = false;
                roleAccountResponseModel.Error.Add("Name not null");
                return roleAccountResponseModel;
            }
            Role role = new Role();
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
                    IdentityResult result = await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
            role.Name = createRoleModel.Name;
            await _roleManager.CreateAsync(role);
            RoleAccountModel roleAccountModel = _mapper.Map<Role,RoleAccountModel>(role);
            roleAccountResponseModel.roleAccountModels.Add(roleAccountModel);
            roleAccountResponseModel.Messege = "Successfully";
            roleAccountResponseModel.Status = true;
            return roleAccountResponseModel;
        }
        public async Task<RoleAccountResponseModel> UpdateRoleUser(UpdateRoleModel updateRoleModel)                                                                                    //UpdateRoleUser
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            Role role = await _roleManager.FindByNameAsync(updateRoleModel.Name);
            if (role == null)
            {
                roleAccountResponseModel.Messege = "Error";
                roleAccountResponseModel.Status = false;
                roleAccountResponseModel.Error.Add("This Name not is database");
            }
            if (roleAccountResponseModel.Messege == null)
            {
                role.Name = updateRoleModel.NewName;
                await _roleManager.UpdateAsync(role);
                RoleAccountModel roleAccountModel = _mapper.Map<Role, RoleAccountModel>(role);
                roleAccountResponseModel.roleAccountModels.Add(roleAccountModel);
                roleAccountResponseModel.Messege = "Successfully";
                roleAccountResponseModel.Status = true;
            }
            return roleAccountResponseModel;
        }
            public async Task<RoleAccountResponseModel> DeleteRoleUser(DeleteRoleModel deleteRoleModel)                                                                              //DeleteRoleUser
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            Role role = await _roleManager.FindByNameAsync(deleteRoleModel.Name);
            if (role == null)
            {
                roleAccountResponseModel.Messege = "Error";
                roleAccountResponseModel.Status = false;
                roleAccountResponseModel.Error.Add("This Name not is database");
            }
            if (roleAccountResponseModel.Messege == null)
            {
                await _roleManager.DeleteAsync(role);
                RoleAccountModel roleAccountModel = _mapper.Map<Role, RoleAccountModel>(role);
                roleAccountResponseModel.roleAccountModels.Add(roleAccountModel);
                roleAccountResponseModel.Messege = "Successfully";
                roleAccountResponseModel.Status = true;
            }
            return roleAccountResponseModel;
        }
        public ActionResult<string> ChangeRoleUser(ChangeRoleUserModel changeRoleUserModel)                                                                        //ChangeRoleUser
        {
            User findUser = _userManager.Users.FirstOrDefault(x => x.Email == changeRoleUserModel.Email);
            Role findRole = _roleManager.Roles.FirstOrDefault(x => x.Name == changeRoleUserModel.NameRole);
            UserInRole userInRole = new UserInRole
            {
                UserId = findUser.Id,
                RoleId = findRole.Id
            };
            return $"{findUser.Id} - {findRole.Id}";
        }
    }
}

