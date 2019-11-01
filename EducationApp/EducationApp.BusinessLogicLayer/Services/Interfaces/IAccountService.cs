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
        Task<RegisterAccountResponseModel> Register(RegisterModel reg);
        Task<LoginAccountResponseModel> Login(LoginModel login);
        Task<ForgotPasswordResponseModel> ForgotPassword(ForgotPassword forgotPassword);
        Task<ConfirmEmailAccountResponseModel> ConfirmEmail(string userId, string code);
        Task<ResetPasswordAccountResponseModel> ResetPassword(ResetPasswordModel reset);
        Task<RefreshTokenAccountResponseModel> RefreshToken(RefreshTokenModel refreshTokenModel, IJwtPrivateKey jwtPrivateKey, IJwtRefresh jwtRefresh);
        RoleAccountResponseModel GetAllRoleUsers();
        Task<RoleAccountResponseModel> CreateRoleUser(CreateRoleModel createRoleModel);
        Task<RoleAccountResponseModel> UpdateRoleUser(UpdateRoleModel updateRoleModel);
        Task<RoleAccountResponseModel> DeleteRoleUser(DeleteRoleModel deleteRoleModel);
        ActionResult<string> ChangeRoleUser(ChangeRoleUserModel changeRoleUserModel);
    }
}
