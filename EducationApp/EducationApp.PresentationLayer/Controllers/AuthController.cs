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
            ConfirmEmailAccountResponseModel confirmEmailAccountResponseModel = await _accountService.ConfirmEmail(confirmEmail);
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
            RegisterAccountResponseModel registerAccountResponseModel = await _accountService.Register(reg);
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
        public async Task<LoginAccountResponseModel> Login([FromBody] LoginModel login)
        {
            LoginAccountResponseModel loginAccountResponseModel =  await _accountService.Login(login);
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
            ForgotPasswordResponseModel forgotPasswordResponseModel = await _accountService.ForgotPassword(forgotPassword);
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
        public async Task<ResetPasswordAccountResponseModel> ResetPassword([FromQuery] ResetPasswordModel reset)
        {
            ResetPasswordAccountResponseModel resetPasswordAccountResponseModel = await _accountService.ResetPassword(reset);
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
        public async Task<RefreshTokenAccountResponseModel> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel)
        {
            RefreshTokenAccountResponseModel refreshTokenAccountResponseModel = await _accountService.RefreshToken(refreshTokenModel);
            return refreshTokenAccountResponseModel;
        }
    }
}