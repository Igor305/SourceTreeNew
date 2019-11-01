using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.PresentationLayer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
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

    }

}