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
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Net.Mail;
using System.Net;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.UserInRole;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _token;
        public AccountService(UserManager<User> userManager, RoleManager<Role> roleManager, IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _mapper = mapper;
            _configuration = configuration;
            _token = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["SecretKey"]));
        }

        private readonly string confirmRegister = "Confirm register";
        private readonly string changePassword = "Change password";

        public async Task <IActionResult> ConfirmEmail(ConfirmEmail confirmEmail)
        {
            ConfirmEmailAccountResponseModel confirmEmailAccountResponseModel = await ValidateConfirmEmail(confirmEmail.UserId, confirmEmail.Code);
            if (confirmEmailAccountResponseModel.Status)
            {
                User user = await _userManager.FindByIdAsync(confirmEmail.UserId);
                IdentityResult result = await _userManager.ConfirmEmailAsync(user, confirmEmail.Code);
            }
            return new RedirectResult(_configuration.GetSection("Client")["RedirectConfirmEmail"]);
        }

        private async Task<ConfirmEmailAccountResponseModel> ValidateConfirmEmail(string userId, string code)
        {
            ConfirmEmailAccountResponseModel confirmEmailAccountResponseModel = new ConfirmEmailAccountResponseModel();
            User user = await _userManager.FindByIdAsync(userId);

            bool isErrorOfNull = string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code);
            bool isErrorOfNotFound = user == null;
            bool isError = isErrorOfNull || isErrorOfNotFound;

            if (isErrorOfNull)
            {
                confirmEmailAccountResponseModel.Error.Add(ResponseConstants.Null);
            }
            if (isErrorOfNotFound)
            {
                confirmEmailAccountResponseModel.Error.Add(ResponseConstants.ErrorNotFoundUser);
            }
            confirmEmailAccountResponseModel.Status = !isError;
            confirmEmailAccountResponseModel.Message = confirmEmailAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return confirmEmailAccountResponseModel;
        }
        public async Task<IActionResult> ResetPassword(ResetPasswordModel reset)
        {          
            ResetPasswordAccountResponseModel resetPasswordAccountResponseModel = await ValidateResetPassword(reset);

            if (resetPasswordAccountResponseModel.Status)
            {
                User user = await _userManager.FindByEmailAsync(reset.Email);
                await _userManager.CheckPasswordAsync(user, reset.Password);
                IdentityResult result = await _userManager.ResetPasswordAsync(user, reset.Code, reset.Password);
            }
            return new RedirectResult(_configuration.GetSection("Client")["RedirectLogin"]);
        }

        public async Task<ResetPasswordAccountResponseModel> ValidateResetPassword(ResetPasswordModel reset)
        {
            ResetPasswordAccountResponseModel resetPasswordAccountResponseModel = new ResetPasswordAccountResponseModel();
            User user = await _userManager.FindByEmailAsync(reset.Email);

            bool isErrorOfNull = string.IsNullOrEmpty(reset.Code) || string.IsNullOrEmpty(reset.Email);
            bool isErrorOfNotFound = user == null;
            bool isError = isErrorOfNull || isErrorOfNotFound;

            if (isErrorOfNull)
            {
                resetPasswordAccountResponseModel.Error.Add(ResponseConstants.Null);
            }
            if (isErrorOfNotFound)
            {
                resetPasswordAccountResponseModel.Error.Add(ResponseConstants.ErrorNotFoundUser);
            }
            resetPasswordAccountResponseModel.Status = !isError;
            resetPasswordAccountResponseModel.Message = resetPasswordAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return resetPasswordAccountResponseModel;
        }

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
                    "Auth",
                    new { userId = user.Id, code },
                    protocol: _httpContextAccessor.HttpContext.Request.Scheme);
                string subject = confirmRegister;
                string message = $"Confirm registration by clicking on the link: <a href={regurl}>Confirm email</a>";
                registerAccountResponseModel.Message = await SendEmail(reg.Email, subject, message);
            }
            return registerAccountResponseModel;
        }

        private RegisterAccountResponseModel ValidateRegister(IdentityResult result)
        {
            RegisterAccountResponseModel registerAccountResponseModel = new RegisterAccountResponseModel();

            if (!result.Succeeded)
            {
                string errorDescription = "";
                var err = result.Errors.ToList();
                err.ForEach(delegate (IdentityError error) { errorDescription = error.Description; });
                registerAccountResponseModel.Message = ResponseConstants.Error;
                registerAccountResponseModel.Error.Add(errorDescription);
            }
            registerAccountResponseModel.Status = result.Succeeded;

            return registerAccountResponseModel;
        }

        public async Task<LoginAccountResponseModel> Login(LoginModel login)
        {
            User user = await _userManager.FindByEmailAsync(login.Email);
            bool confirmPassword = await _userManager.CheckPasswordAsync(user, login.Password);
            LoginAccountResponseModel loginAccountResponseModel = await ValidateLogin(login);

            if (confirmPassword)
            {
                IList<string> roleUser = await _userManager.GetRolesAsync(user);
                if (roleUser == null)
                {
                    await _userManager.AddToRoleAsync(user, _configuration.GetSection("Role")["Client"]);
                    roleUser = await _userManager.GetRolesAsync(user);
                }

                List<Claim> claimsAccessToken = new List<Claim>();
                claimsAccessToken.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claimsAccessToken.Add(new Claim(ClaimTypes.Email, user.Email));
                claimsAccessToken.Add(new Claim(ClaimTypes.Hash, user.PasswordHash));
                foreach (var role in roleUser)
                {
                    claimsAccessToken.Add(new Claim(ClaimTypes.Role, role));
                }

                string accessToken = GenerateToken(claimsAccessToken, int.Parse(_configuration.GetSection("JWT")["LifeTimeAccessToken"]));

                List<Claim> claimsRefeshToken = new List<Claim>();
                claimsRefeshToken.Add(new Claim(ClaimTypes.Authentication, accessToken));
                claimsRefeshToken.Add(new Claim(ClaimTypes.Email, user.Email));

                string refreshToken = GenerateToken(claimsRefeshToken, int.Parse(_configuration.GetSection("JWT")["LifeTimeRefreshToken"]));
                loginAccountResponseModel.AccessToken = accessToken;
                loginAccountResponseModel.RefreshToken = refreshToken;
            }
            return loginAccountResponseModel;
        }
        private async Task<LoginAccountResponseModel> ValidateLogin(LoginModel login)
        {
            LoginAccountResponseModel loginAccountResponseModel = new LoginAccountResponseModel();

            User user = await _userManager.FindByEmailAsync(login.Email);
            bool confirmpass = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!confirmpass)
            {
                loginAccountResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            loginAccountResponseModel.Status = confirmpass;
            loginAccountResponseModel.Message = loginAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

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
                    "Auth",
                new { user.Email, forgotPassword.Password, forgotPassword.ConfirmPassword, code },
                protocol: _httpContextAccessor.HttpContext.Request.Scheme);
                string subject = changePassword;
                string message = $"To reset your password, follow the link: <a href='{callbackUrl}'>Change password</a>";
                forgotPasswordResponseModel.Message = await SendEmail(forgotPassword.Email, subject, message);
            }
            return forgotPasswordResponseModel;
        }

        private async Task<ForgotPasswordResponseModel> ValidateForgotPassword(ForgotPassword forgotPassword)
        {
            ForgotPasswordResponseModel forgotPasswordResponseModel = new ForgotPasswordResponseModel();
            User user = await _userManager.FindByNameAsync(forgotPassword.Email);

            bool isSuccessfully = user != null || await _userManager.IsEmailConfirmedAsync(user);

            if (!isSuccessfully)
            {
                forgotPasswordResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            forgotPasswordResponseModel.Status = isSuccessfully;
            forgotPasswordResponseModel.Message = forgotPasswordResponseModel.Status ? ResponseConstants.ConfirmEmail : ResponseConstants.Error;

            return forgotPasswordResponseModel;
        }

        public async Task<RefreshTokenAccountResponseModel> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(jwtEncodedString: refreshTokenModel.TokenString);
            string decodingtoken = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            User user = await _userManager.FindByEmailAsync(decodingtoken);
            RefreshTokenAccountResponseModel refreshTokenAccountResponseModel = ValidateRefreshToken(user);
            if (refreshTokenAccountResponseModel.Status)
            {
                IList<string> roleUser = await _userManager.GetRolesAsync(user);
                if (roleUser == null)
                {
                    await _userManager.AddToRoleAsync(user, _configuration.GetSection("Role")["Client"]);
                    roleUser = await _userManager.GetRolesAsync(user);
                }

                List<Claim> claimsAccessToken = new List<Claim>();
                claimsAccessToken.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claimsAccessToken.Add(new Claim(ClaimTypes.Email, user.Email));
                claimsAccessToken.Add(new Claim(ClaimTypes.Hash, user.PasswordHash));
                foreach (var role in roleUser)
                {
                    claimsAccessToken.Add(new Claim(ClaimTypes.Role, role));
                }

                string accessToken = GenerateToken(claimsAccessToken, int.Parse(_configuration.GetSection("JWT")["LifeTimeAccessToken"]));

                List<Claim> claimsRefeshToken = new List<Claim>();
                claimsRefeshToken.Add(new Claim(ClaimTypes.Authentication, accessToken));
                claimsRefeshToken.Add(new Claim(ClaimTypes.Email, user.Email));

                string refreshToken = GenerateToken(claimsRefeshToken, int.Parse(_configuration.GetSection("JWT")["LifeTimeRefreshToken"]));
                refreshTokenAccountResponseModel.AccessToken = accessToken;
                refreshTokenAccountResponseModel.RefreshToken = refreshToken;
            }
            return refreshTokenAccountResponseModel;
        }

        private RefreshTokenAccountResponseModel ValidateRefreshToken(User user)
        {
            RefreshTokenAccountResponseModel refreshTokenAccountResponseModel = new RefreshTokenAccountResponseModel();
            bool isErrorOfNull = user == null;

            if (isErrorOfNull)
            {
                refreshTokenAccountResponseModel.Error.Add(ResponseConstants.ErrorNotFoundUser);
            }
            refreshTokenAccountResponseModel.Status = !isErrorOfNull;
            refreshTokenAccountResponseModel.Message = refreshTokenAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return refreshTokenAccountResponseModel;
        }

        public RoleAccountResponseModel GetAllRoles()
        {
            RoleAccountResponseModel roleAccountResponseModel = ValidateGetAllRoles();

            List<Role> roles = _roleManager.Roles.ToList();
            List<RoleAccountModel> roleAccountModels = _mapper.Map<List<Role>, List<RoleAccountModel>>(roles);
            roleAccountResponseModel.roleAccountModels = roleAccountModels;

            return roleAccountResponseModel;
        }

        private RoleAccountResponseModel ValidateGetAllRoles()
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            roleAccountResponseModel.Status = true;
            roleAccountResponseModel.Message = ResponseConstants.Successfully;

            return roleAccountResponseModel;
        }

        public async Task<RoleAccountResponseModel> CreateRole(CreateRoleModel createRoleModel)
        {
            RoleAccountResponseModel roleAccountResponseModel = ValidateCreateRole(createRoleModel);
            Role role = new Role();

            if (roleAccountResponseModel.Status)
            {
                role.Name = createRoleModel.Name;
                await _roleManager.CreateAsync(role);
                RoleAccountModel roleAccountModel = _mapper.Map<Role, RoleAccountModel>(role);
                roleAccountResponseModel.roleAccountModels.Add(roleAccountModel);
            }
            return roleAccountResponseModel;
        }

        private RoleAccountResponseModel ValidateCreateRole(CreateRoleModel createRoleModel)
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();

            bool isErrorOfNull = string.IsNullOrEmpty(createRoleModel.Name);
            if (isErrorOfNull)
            {
                roleAccountResponseModel.Error.Add(ResponseConstants.Null);
            }
            roleAccountResponseModel.Status = !isErrorOfNull;
            roleAccountResponseModel.Message = roleAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return roleAccountResponseModel;
        }

        public async Task<UserInRoleResponseModel> AddingRoleUser(ChangeRoleUserModel changeRoleUserModel)
        {
            User user = _userManager.Users.FirstOrDefault(x => x.Email == changeRoleUserModel.Email);
            Role role = _roleManager.Roles.FirstOrDefault(x => x.Name == changeRoleUserModel.NameRole);
            UserInRoleResponseModel userInRoleResponseModel = ValidateChangeRoleUser(user, role);
            if (userInRoleResponseModel.Status)
            {
                IdentityResult identityResult = await _userManager.AddToRoleAsync(user, role.Name);

                UserInRoleModel userInRoleModel = new UserInRoleModel();
                userInRoleModel.UserId = user.Id;
                userInRoleModel.RoleId = role.Id;
                userInRoleResponseModel.UserInRoleModels.Add(userInRoleModel);
            }
            return userInRoleResponseModel;
        }

        public async Task<UserInRoleResponseModel> TakingAwayUserRole(ChangeRoleUserModel changeRoleUserModel)
        {
            User user = _userManager.Users.FirstOrDefault(x => x.Email == changeRoleUserModel.Email);
            Role role = _roleManager.Roles.FirstOrDefault(x => x.Name == changeRoleUserModel.NameRole);
            UserInRoleResponseModel userInRoleResponseModel = ValidateChangeRoleUser(user, role);
            if (userInRoleResponseModel.Status)
            {
                IdentityResult identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name);

                UserInRoleModel userInRoleModel = new UserInRoleModel();
                userInRoleModel.UserId = user.Id;
                userInRoleModel.RoleId = role.Id;
                userInRoleResponseModel.UserInRoleModels.Add(userInRoleModel);
            }
            return userInRoleResponseModel;
        }

        private UserInRoleResponseModel ValidateChangeRoleUser(User user, Role role)
        {
            UserInRoleResponseModel userInRoleResponseModel = new UserInRoleResponseModel();
            bool isErrorOfNotFoundUser = user == null;
            bool isErrorOfNotFoundRole = role == null;
            bool isError = isErrorOfNotFoundUser || isErrorOfNotFoundRole;

            if (isErrorOfNotFoundUser)
            {
                userInRoleResponseModel.Error.Add(ResponseConstants.ErrorNotFoundUser);
            }
            if (isErrorOfNotFoundRole)
            {
                userInRoleResponseModel.Error.Add(ResponseConstants.ErrorNotFoundRole);
            }
            userInRoleResponseModel.Status = !isError;
            userInRoleResponseModel.Message = userInRoleResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return userInRoleResponseModel;
        }

        public async Task<RoleAccountResponseModel> UpdateRole(UpdateRoleModel updateRoleModel)
        {
            Role role = await _roleManager.FindByNameAsync(updateRoleModel.Name);
            RoleAccountResponseModel roleAccountResponseModel = ValidateUpdateRole(role);

            if (roleAccountResponseModel.Status)
            {
                role.Name = updateRoleModel.NewName;
                await _roleManager.UpdateAsync(role);
                RoleAccountModel roleAccountModel = _mapper.Map<Role, RoleAccountModel>(role);
                roleAccountResponseModel.roleAccountModels.Add(roleAccountModel);
            }
            return roleAccountResponseModel;
        }

        private RoleAccountResponseModel ValidateUpdateRole(Role role)
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            bool isErrorOfNull = role == null;

            if (isErrorOfNull)
            {
                roleAccountResponseModel.Error.Add(ResponseConstants.Null);
            }
            roleAccountResponseModel.Status = !isErrorOfNull;
            roleAccountResponseModel.Message = roleAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return roleAccountResponseModel;
        }

        public async Task<RoleAccountResponseModel> DeleteRole(DeleteRoleModel deleteRoleModel)
        {
            Role role = await _roleManager.FindByNameAsync(deleteRoleModel.Name);
            RoleAccountResponseModel roleAccountResponseModel = ValidateDeleteRole(role);

            if (roleAccountResponseModel.Status)
            {
                await _roleManager.DeleteAsync(role);
                RoleAccountModel roleAccountModel = _mapper.Map<Role, RoleAccountModel>(role);
                roleAccountResponseModel.roleAccountModels.Add(roleAccountModel);
            }
            return roleAccountResponseModel;
        }

        private RoleAccountResponseModel ValidateDeleteRole(Role role)
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            bool isErrorOfNull = role == null;

            if (isErrorOfNull)
            {
                roleAccountResponseModel.Error.Add(ResponseConstants.Null);
            }
            roleAccountResponseModel.Status = !isErrorOfNull;
            roleAccountResponseModel.Message = roleAccountResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return roleAccountResponseModel;
        }

        private async Task<string> SendEmail(string email, string subject, string body)
        {
            string message = ResponseConstants.ConfirmEmail + " " + email;
            try
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential();
                    credential.UserName = _configuration.GetSection("SMTP")["Email"];
                    credential.Password = _configuration.GetSection("SMTP")["Password"];

                    client.Credentials = credential;
                    client.Host = _configuration.GetSection("SMTP")["Host"];
                    client.Port = int.Parse(_configuration.GetSection("SMTP")["Port"]);
                    client.EnableSsl = true;

                    using (var emailMessage = new MailMessage())
                    {
                        var mailAddress = new MailAddress(email);
                        emailMessage.To.Add(mailAddress);
                        emailMessage.From = new MailAddress(_configuration.GetSection("SMTP")["Email"]);
                        emailMessage.Subject = subject;
                        emailMessage.IsBodyHtml = true;
                        emailMessage.Body = body;

                        client.Send(emailMessage);
                    }
                }
                await Task.CompletedTask;
            }
            catch
            {
                message = ResponseConstants.ErrorSendEmail;
            }
            return message;
        }

        private string GenerateToken(List<Claim> claims, int lifetime)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT")["Issuer"],
                audience: _configuration.GetSection("JWT")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(lifetime),
                signingCredentials: new SigningCredentials(
                       _token,
                       SecurityAlgorithms.HmacSha256)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}