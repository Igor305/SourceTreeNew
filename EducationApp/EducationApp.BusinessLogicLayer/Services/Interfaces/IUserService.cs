using EducationApp.BusinessLogicLayer.Models.ResponseModels.User;
using EducationApp.BusinessLogicLayer.Models.User;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> GetAllIsDeleted();
        Task<UserResponseModel> GetAll();
        Task<UserResponseModel> Create(CreateUserModel createUserModel);
        Task<UserResponseModel> Update(UpdateUserModel updateUserModel);
        Task<UserResponseModel> Delete(DeleteModel deleteModel);
        Task<UserResponseModel> FinalRemoval(DeleteModel deleteModel);
    }
}
