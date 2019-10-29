using EducationApp.BusinessLogicLayer.Models.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;
using EducationApp.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthAccountResponseModel> GetAuth();
        Task<LoginAccountResponseModel> PostAuth(LoginModel login, IJwtPrivateKey jwtPrivateKey, IJwtRefresh jwtRefresh);
        Task<RegisterAccountResponseModel> Register(RegisterModel reg);
        Task<ForgotPasswordResponseModel> ForgotPassword(ForgotPassword forgotPassword);
        Task<ConfirmEmailAccountResponseModel> ConfirmEmail(string userId, string code);
        Task<ResetPasswordAccountResponseModel> ResetPassword(ResetPasswordModel reset);
        Task<RefreshTokenAccountResponseModel> RefreshToken(RefreshTokenModel refreshTokenModel, IJwtPrivateKey jwtPrivateKey, IJwtRefresh jwtRefresh);
        Task<RoleAccountResponseModel> GetAllRoleUsers();
        Task<RoleAccountResponseModel> CreateRoleUsers(CreateRoleModel createRoleModel);
        Task<RoleAccountResponseModel> DeleteRoleUsers(DeleteRoleModel deleteRoleModel);
        Task<ActionResult<string>> ChangeRoleUser(ChangeRoleUserModel changeRoleUserModel);
    }
}
