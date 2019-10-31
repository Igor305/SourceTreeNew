using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using EducationApp.BusinessLogicLayer.Models.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;

namespace EducationApp.PresentationLayer.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Authentication
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAuth
        ///
        /// </remarks>
        [HttpGet("Auth")]
        [Authorize]
        public AuthAccountResponseModel GetAuth()
        {
            AuthAccountResponseModel authAccountResponseModel = _accountService.GetAuth();
            return authAccountResponseModel;
        }
        /// <summary>
        /// Get All Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAllRole
        ///
        /// </remarks>
        [HttpGet("GetAllRole")]
        public RoleAccountResponseModel GetAllRoleUsers()
        {
            RoleAccountResponseModel roleAccountResponseModel = _accountService.GetAllRoleUsers();
            return roleAccountResponseModel;
        }
        /// <summary>
        ///  Confirm Password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/ConfirmEmail
        /// 
        ///     "Email":"karakymba@gmail.com",
        ///     
        /// </remarks>
        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<ConfirmEmailAccountResponseModel> ConfirmEmail([FromQuery]ConfirmEmail confirmEmail)
        {
            ConfirmEmailAccountResponseModel confirmEmailAccountResponseModel = new ConfirmEmailAccountResponseModel();
            if (ModelState.IsValid)
            {
                confirmEmailAccountResponseModel = await _accountService.ConfirmEmail(confirmEmail.userId, confirmEmail.code);
                return confirmEmailAccountResponseModel;
            }
            confirmEmailAccountResponseModel.Messege = "Error";
            confirmEmailAccountResponseModel.Status = false;
            confirmEmailAccountResponseModel.Error.Add("Not IsValid");
            return confirmEmailAccountResponseModel;
        }
        /// <summary>
        ///  Log In
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/PostAuth
        ///     
        ///     "Email":"karakymba@gmail.com",
        ///     "Password":"karaganda"
        ///
        /// </remarks>
        [HttpPost("Auth")]
        [AllowAnonymous]
        public async Task<LoginAccountResponseModel> Login([FromBody] LoginModel login, [FromServices] IJwtPrivateKey jwtPrivateKey, [FromServices] IJwtRefresh jwtRefresh)
        {
            LoginAccountResponseModel loginAccountResponseModel = new LoginAccountResponseModel();
            if (ModelState.IsValid)
            {

                loginAccountResponseModel = await _accountService.Login(login, jwtPrivateKey, jwtRefresh);
                return loginAccountResponseModel; 
            }
            loginAccountResponseModel.Messege = "Error";
            loginAccountResponseModel.Status = false;
            loginAccountResponseModel.Error.Add("Not IsValid");
            return loginAccountResponseModel;
        }
        /// <summary>
        ///  Register
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post/Register
        ///
        ///     "Email":"karakymba@gmail.com",
        ///     "Password":"karaganda",
        ///     "PasswordConfirm":"karaganda"
        ///     
        /// </remarks>
        [HttpPost("Register")]
        public async Task<RegisterAccountResponseModel> Register([FromBody]RegisterModel reg)
        {
            RegisterAccountResponseModel registerAccountResponseModel = new RegisterAccountResponseModel();
            if (ModelState.IsValid)
            {
                registerAccountResponseModel = await _accountService.Register(reg);
                return registerAccountResponseModel;
            }
            registerAccountResponseModel.Messege = "Error";
            registerAccountResponseModel.Status = false;
            registerAccountResponseModel.Error.Add("Not IsValid");
            return registerAccountResponseModel;
        }
        /// <summary>
        ///  Forgot Password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post/ForgotPassword
        ///
        ///     "Email":"karakymba@gmail.com"
        ///     
        /// </remarks>
        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<ForgotPasswordResponseModel> ForgotPassword([FromBody]ForgotPassword forgotPassword)
        {
            ForgotPasswordResponseModel forgotPasswordResponseModel = new ForgotPasswordResponseModel();
            if (ModelState.IsValid)
            {
                forgotPasswordResponseModel = await _accountService.ForgotPassword(forgotPassword);
                return forgotPasswordResponseModel; 
            }
            forgotPasswordResponseModel.Messege = "Error";
            forgotPasswordResponseModel.Status = false;
            forgotPasswordResponseModel.Error.Add("Not IsValid");
            return forgotPasswordResponseModel;
        }
      
        /// <summary>
        ///  Reset Password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/ResetPassword
        /// 
        ///     "Email":"karakymba@gmail.com",
        ///     "Password":"karaganda",
        ///     "PasswordConfirm":"karaganda",
        ///     "Code":"ioprewthjypoiwreyortpo"
        ///     
        /// </remarks>
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<ResetPasswordAccountResponseModel> ResetPassword([FromBody] ResetPasswordModel reset)
        {
            ResetPasswordAccountResponseModel resetPasswordAccountResponseModel = new ResetPasswordAccountResponseModel();
            if (ModelState.IsValid)
            {
                resetPasswordAccountResponseModel = await _accountService.ResetPassword(reset);
                return resetPasswordAccountResponseModel; 
            }
            resetPasswordAccountResponseModel.Messege = "Error";
            resetPasswordAccountResponseModel.Status = false;
            resetPasswordAccountResponseModel.Error.Add("Not IsValid");
            return resetPasswordAccountResponseModel;
        }
        /// <summary>
        ///  Refresh Token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/RefreshToken
        /// 
        ///     "token":"karakymba@gmail.com"
        ///     
        /// </remarks>
        [HttpPost("RefreshToken")]
        public async Task<RefreshTokenAccountResponseModel> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel, [FromServices] IJwtPrivateKey jwtPrivateKey, [FromServices] IJwtRefresh jwtRefresh)
        {
            RefreshTokenAccountResponseModel refreshTokenAccountResponseModel = new RefreshTokenAccountResponseModel();
            if (ModelState.IsValid)
            {
                refreshTokenAccountResponseModel = await _accountService.RefreshToken(refreshTokenModel, jwtPrivateKey, jwtRefresh);
                return refreshTokenAccountResponseModel;
            }
            refreshTokenAccountResponseModel.Messege = "Error";
            refreshTokenAccountResponseModel.Status = false;
            refreshTokenAccountResponseModel.Error.Add("Not IsValid");
            return refreshTokenAccountResponseModel;
        }
        /// <summary>
        ///  Create Roles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/CreateRoleUsers
        /// 
        ///     "Name":"Slave"
        ///     
        /// </remarks>
        [HttpPost("CreateRole")]
        public async Task<RoleAccountResponseModel> CreateRoleUsers([FromBody] CreateRoleModel createRoleModel)
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            if (ModelState.IsValid)
            {
                roleAccountResponseModel = await _accountService.CreateRoleUser(createRoleModel);
                return roleAccountResponseModel;
            }
            roleAccountResponseModel.Messege = "Error";
            roleAccountResponseModel.Status = false;
            roleAccountResponseModel.Error.Add("Not IsValid");
            return roleAccountResponseModel;
        }
        /// <summary>
        ///  Update Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put/UpdateRoleUser
        /// 
        ///     "Name":"Slave"
        ///     
        /// </remarks>
        [HttpPut("UpdateRole")]
        public async Task<RoleAccountResponseModel> UpdateRoleUser([FromBody] UpdateRoleModel updateRoleModel)
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            if (ModelState.IsValid)
            {
                roleAccountResponseModel = await _accountService.UpdateRoleUser(updateRoleModel);
                return roleAccountResponseModel;
            }
            roleAccountResponseModel.Messege = "Error";
            roleAccountResponseModel.Status = false;
            roleAccountResponseModel.Error.Add("Not IsValid");
            return roleAccountResponseModel;
        }
        /// <summary>
        ///  DeleteRoleUsers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete/DeleteRoleUsers
        /// 
        ///     "Name":"Slave"
        ///     
        /// </remarks>
        [HttpDelete("DeleteRole")]
        public async Task<RoleAccountResponseModel> DeleteRoleUser([FromBody] DeleteRoleModel deleteRoleModel)
        {
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            if (ModelState.IsValid)
            {
                roleAccountResponseModel = await _accountService.DeleteRoleUser(deleteRoleModel);
                return roleAccountResponseModel;
            }
            roleAccountResponseModel.Messege = "Error";
            roleAccountResponseModel.Status = false;
            roleAccountResponseModel.Error.Add("Not IsValid");
            return roleAccountResponseModel;
        }
    }
}