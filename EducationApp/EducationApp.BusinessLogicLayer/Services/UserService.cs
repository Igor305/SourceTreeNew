using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.User;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserResponseModel> GetAllIsDeleted()
        {
            List<User> allIsDeleted = await _userRepository.GetAllIsDeleted();
            List<UserModel> userModels = _mapper.Map<List<User>, List<UserModel>>(allIsDeleted);
            UserResponseModel userResponseModel = new UserResponseModel();
            userResponseModel.Messege = "Successfully";
            userResponseModel.Status = true;
            userResponseModel.UserModels = userModels;
            return userResponseModel;
        }
        public async Task<UserResponseModel> GetAll()
        {
            List<User> all = await _userRepository.GetAll();
            List<UserModel> userModels = _mapper.Map <List<User>, List<UserModel>> (all);
            UserResponseModel userResponseModel = new UserResponseModel();
            userResponseModel.Messege = "Successfully";
            userResponseModel.Status = true;
            userResponseModel.UserModels = userModels;
            return userResponseModel;
        }
        public async Task<UserResponseModel> Create(CreateUserModel createUserModel)
        { 
            User user = _mapper.Map<CreateUserModel, User>(createUserModel);
            user.CreateDateTime = DateTime.Now;
            user.UpdateDateTime = user.CreateDateTime;
            await _userRepository.Create(user);
            UserModel userModel = _mapper.Map<User, UserModel>(user);
            UserResponseModel userResponseModel = new UserResponseModel();
            userResponseModel.Messege = "Successfully";
            userResponseModel.Status = true;
            userResponseModel.UserModels.Add(userModel);
            return userResponseModel;
        }
        public async Task<UserResponseModel> Update(UpdateUserModel updateUserModel)
        {
            User findUser = await _userRepository.GetById(updateUserModel.Id);
            _mapper.Map<UpdateUserModel, User>(updateUserModel);
            findUser.UpdateDateTime = DateTime.Now;
            await _userRepository.Update(findUser);
            UserModel userModel = _mapper.Map<User, UserModel>(findUser);
            UserResponseModel userResponseModel = new UserResponseModel();
            userResponseModel.Messege = "Successfully";
            userResponseModel.Status = true;
            userResponseModel.UserModels.Add(userModel);
            return userResponseModel;
        }
        public async Task<UserResponseModel> Delete(DeleteModel deleteModel)
        {
            User findUser = await _userRepository.GetById(deleteModel.Id);
            findUser.IsDeleted = true;
            await _userRepository.Update(findUser);
            UserModel userModel = _mapper.Map<User, UserModel>(findUser);
            UserResponseModel userResponseModel = new UserResponseModel();
            userResponseModel.Messege = "Successfully";
            userResponseModel.Status = true;
            userResponseModel.UserModels.Add(userModel);
            return userResponseModel;
        }
        public async Task<UserResponseModel> FinalRemoval(DeleteModel deleteModel)
        {
            User findUser = await _userRepository.GetByIdAllIsDeleted(deleteModel.Id);
            await _userRepository.Delete(findUser);
            UserModel userModel = _mapper.Map<User, UserModel>(findUser);
            UserResponseModel userResponseModel = new UserResponseModel();
            userResponseModel.Messege = "Successfully";
            userResponseModel.Status = true;
            userResponseModel.UserModels.Add(userModel);
            return userResponseModel;
        }
    }
}
