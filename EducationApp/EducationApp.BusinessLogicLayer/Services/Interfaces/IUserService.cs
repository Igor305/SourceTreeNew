using EducationApp.BusinessLogicLayer.Models.ResponseModels.User;
using EducationApp.BusinessLogicLayer.Models.User;
using System;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> GetAll();
        Task<UserResponseModel> GetAllWithoutRemove();
        Task<UserResponseModel> GetById(Guid id);
        Task<UserResponseModel> Create(CreateUserModel createUserModel);
        Task<UserResponseModel> Update(Guid id, CreateUserModel createUserModel);
        Task<UserResponseModel> Delete(Guid id);
        Task<UserResponseModel> FinalRemoval(Guid id);
    }
}
