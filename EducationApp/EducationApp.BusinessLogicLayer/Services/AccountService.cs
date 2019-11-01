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
using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.BusinessLogicLayer.Helpers;

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

        /*public AuthAccountResponseModel GetAuth()                                                                                                       
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
        }*/
        public async Task<RegisterAccountResponseModel> Register(RegisterModel reg)
        {
            User user = new User();
            user.Email = reg.Email;
            user.UserName = reg.Email;

            IdentityResult result = await _userManager.CreateAsync(user, reg.Password);
            RegisterAccountResponseModel registerAccountResponseModel = ValidateRegister(result);
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
            }
            return registerAccountResponseModel;
        }
        private RegisterAccountResponseModel ValidateRegister(IdentityResult result)
        {
            RegisterAccountResponseModel registerAccountResponseModel = new RegisterAccountResponseModel();
            if (!result.Succeeded)
            {
                registerAccountResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            if (registerAccountResponseModel.Error.Count == 0)
            {
                registerAccountResponseModel.Status = true;
            }
            registerAccountResponseModel.Messege = registerAccountResponseModel.Status ? ResponseConstants.ConfirmEmail : ResponseConstants.Error;
            return registerAccountResponseModel;
        }
        public async Task<LoginAccountResponseModel> Login(LoginModel login)
        {    
            User user = await _userManager.FindByEmailAsync(login.Email);
            bool confirmpass = await _userManager.CheckPasswordAsync(user, login.Password);
            LoginAccountResponseModel loginAccountResponseModel = await ValidateLogin(login);
            if (confirmpass)
            {
                List<Claim> claimsAccessToken = new List<Claim>();
                claimsAccessToken.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claimsAccessToken.Add(new Claim(ClaimTypes.Email, user.Email));
                claimsAccessToken.Add(new Claim(ClaimTypes.Hash, user.PasswordHash));
                claimsAccessToken.Add(new Claim(ClaimTypes.Role, "User"));

                JwtHelper jwtHelper = new JwtHelper();
                string accessToken = jwtHelper.GenerateToken(claimsAccessToken, 30);

                List<Claim> claimsRefeshToken = new List<Claim>();
                claimsRefeshToken.Add(new Claim(ClaimTypes.Authentication, accessToken));
                claimsRefeshToken.Add(new Claim(ClaimTypes.Email, user.Email));

                string refreshToken = jwtHelper.GenerateToken(claimsRefeshToken, 50000);
                loginAccountResponseModel.AccessToken = accessToken;
                loginAccountResponseModel.RefreshToken = refreshToken;
            }
            return loginAccountResponseModel;
        }

        private async Task<LoginAccountResponseModel> ValidateLogin (LoginModel login)
        {
            LoginAccountResponseModel loginAccountResponseModel = new LoginAccountResponseModel();

            User user = await _userManager.FindByEmailAsync(login.Email);
            bool confirmpass = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!confirmpass)
            {
                loginAccountResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            if (loginAccountResponseModel.Error.Count == 0)
            {
                loginAccountResponseModel.Status = true;
            }
            loginAccountResponseModel.Messege = loginAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return loginAccountResponseModel;
        }

        public async Task<ForgotPasswordResponseModel> ForgotPassword(ForgotPassword forgotPassword)                                                                                 
        {
            User user = await _userManager.FindByNameAsync(forgotPassword.Email);
            ForgotPasswordResponseModel forgotPasswordResponseModel = await ValidateForgotPassword(forgotPassword);
            if (user != null || (await _userManager.IsEmailConfirmedAsync(user)))
            {
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
            }
            return forgotPasswordResponseModel;
        }

        private async Task<ForgotPasswordResponseModel> ValidateForgotPassword(ForgotPassword forgotPassword)
        {
            ForgotPasswordResponseModel forgotPasswordResponseModel = new ForgotPasswordResponseModel();
            User user = await _userManager.FindByNameAsync(forgotPassword.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                forgotPasswordResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            if (forgotPasswordResponseModel.Error.Count == 0)
            {
                forgotPasswordResponseModel.Status = true;
            }
            forgotPasswordResponseModel.Messege = forgotPasswordResponseModel.Status ? ResponseConstants.ConfirmEmail : ResponseConstants.Error;
            return forgotPasswordResponseModel;
        }
        public async Task<ConfirmEmailAccountResponseModel> ConfirmEmail(string userId, string code)                                                                           
        {
            ConfirmEmailAccountResponseModel confirmEmailAccountResponseModel = new ConfirmEmailAccountResponseModel();
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                confirmEmailAccountResponseModel.Error.Add(ResponseConstants.Null);
            }
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                confirmEmailAccountResponseModel.Error.Add(ResponseConstants.ErrorNotFound);
            }
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                confirmEmailAccountResponseModel.Status = true;
            }
            confirmEmailAccountResponseModel.Messege = confirmEmailAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;
            return confirmEmailAccountResponseModel;
        }
        public async Task<ResetPasswordAccountResponseModel> ResetPassword(ResetPasswordModel reset)                                                                             
        {
            ResetPasswordAccountResponseModel resetPasswordAccountResponseModel = new ResetPasswordAccountResponseModel();
            if (string.IsNullOrEmpty(reset.Code) || string.IsNullOrEmpty(reset.Email))
            {
                resetPasswordAccountResponseModel.Error.Add(ResponseConstants.Null);
            }
            User user = await _userManager.FindByNameAsync(reset.Email);
            if (user == null)
            {
                resetPasswordAccountResponseModel.Error.Add(ResponseConstants.ErrorNotFound);
            }
            await _userManager.CheckPasswordAsync(user, reset.Password);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, reset.Code, reset.Password);
            if (result.Succeeded)
            {
                resetPasswordAccountResponseModel.Status = true;
            }
            resetPasswordAccountResponseModel.Messege = resetPasswordAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;
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

