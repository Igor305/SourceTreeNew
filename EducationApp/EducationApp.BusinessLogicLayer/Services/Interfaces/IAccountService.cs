using EducationApp.BusinessLogicLayer.Models.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.UserInRole;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterAccountResponseModel> Register(RegisterModel reg);
        Task<LoginAccountResponseModel> Login(LoginModel login);
        Task<ForgotPasswordResponseModel> ForgotPassword(ForgotPassword forgotPassword);
        Task<IActionResult> ConfirmEmail(ConfirmEmail confirmEmail);
        Task<ResetPasswordAccountResponseModel> ResetPassword(ResetPasswordModel reset);
        Task<RefreshTokenAccountResponseModel> RefreshToken(RefreshTokenModel refreshTokenModel);
        RoleAccountResponseModel GetAllRoles();
        Task<RoleAccountResponseModel> CreateRole(CreateRoleModel createRoleModel);
        Task<UserInRoleResponseModel> AddingRoleUser(ChangeRoleUserModel changeRoleUserModel);
        Task<UserInRoleResponseModel> TakingAwayUserRole(ChangeRoleUserModel changeRoleUserModel);
        Task<RoleAccountResponseModel> UpdateRole(UpdateRoleModel updateRoleModel);
        Task<RoleAccountResponseModel> DeleteRole(DeleteRoleModel deleteRoleModel);

    }
}
