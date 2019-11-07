using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.User;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.UserInRole;
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

        public async Task<UserResponseModel> GetAll()
        {
            UserResponseModel userResponseModel = ValidateSuccessfully();

            List<User> allIsDeleted = await _userRepository.GetAll();
            List<UserModel> userModels = _mapper.Map<List<User>, List<UserModel>>(allIsDeleted);
            userResponseModel.UserModels = userModels;

            return userResponseModel;
        }

        public async Task<UserResponseModel> GetAllWithoutRemove()
        {
            UserResponseModel userResponseModel = ValidateSuccessfully();

            List<User> all = await _userRepository.GetAllWithoutRemove();
            List<UserModel> userModels = _mapper.Map <List<User>, List<UserModel>> (all);
            userResponseModel.UserModels = userModels;

            return userResponseModel;
        }

        private UserResponseModel ValidateSuccessfully()
        {
            UserResponseModel userResponseModel = new UserResponseModel();

            userResponseModel.Status = true;
            userResponseModel.Messege = ResponseConstants.Successfully;

            return userResponseModel;
        }

        public async Task<UserResponseModel> GetById(Guid id)
        {
            UserResponseModel userResponseModel = await ValidateGetById(id);
            if (userResponseModel.Status)
            {
                User user = await _userRepository.GetById(id);
                UserModel userModel = _mapper.Map<User, UserModel>(user);
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }

        private async Task<UserResponseModel> ValidateGetById(Guid id)
        {
            UserResponseModel userResponseModel = new UserResponseModel();

            bool isExist = await _userRepository.CheckById(id);
            if (!isExist)
            {
                userResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            userResponseModel.Status = isExist;
            userResponseModel.Messege = userResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return userResponseModel;
        }

        public async Task<UserResponseModel> Create(CreateUserModel createUserModel)
        {
            UserResponseModel userResponseModel = ValidateCreate(createUserModel);

            if (userResponseModel.Status)
            {
                User user = _mapper.Map<CreateUserModel, User>(createUserModel);
                user.CreateDateTime = DateTime.Now;
                user.UpdateDateTime = user.CreateDateTime;
                await _userRepository.Create(user);
                UserModel userModel = _mapper.Map<User, UserModel>(user);
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }

        private UserResponseModel ValidateCreate(CreateUserModel createUserModel)
        {
            UserResponseModel userResponseModel = new UserResponseModel();

            bool isErrorOfNull = string.IsNullOrEmpty(createUserModel.Email) || string.IsNullOrEmpty(createUserModel.FirstName) || string.IsNullOrEmpty(createUserModel.LastName);
            bool isWarningOfNull = string.IsNullOrEmpty(createUserModel.PhoneNumber);

            if (isErrorOfNull) 
            {
                userResponseModel.Error.Add(ResponseConstants.Null);
            }
            if (isWarningOfNull)
            {
                userResponseModel.Warning.Add(ResponseConstants.Null);
            }
            userResponseModel.Status = !isErrorOfNull;
            userResponseModel.Messege = userResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return userResponseModel;
        }

        public async Task<UserResponseModel> Update(Guid id, CreateUserModel createUserModel)
        {
            UserResponseModel userResponseModel = new UserResponseModel();

            if (userResponseModel.Status)
            {
                User user = await _userRepository.GetById(id);
                _mapper.Map(createUserModel,user);
                user.UpdateDateTime = DateTime.Now;
                await _userRepository.Update(user);
                UserModel userModel = _mapper.Map<User, UserModel>(user);
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }

        private async Task<UserResponseModel> ValidateUpdate(Guid id, CreateUserModel createUserModel)
        {
            UserResponseModel userResponseModel = await ValidateGetById(id);
            userResponseModel = ValidateCreate(createUserModel);

            return userResponseModel;
        }

        public async Task<UserResponseModel> Delete(Guid id)
        {
            UserResponseModel userResponseModel = await ValidateGetById(id);
            
            if (userResponseModel.Status)
            {
                User findUser = await _userRepository.GetById(id);
                findUser.IsDeleted = true;
                await _userRepository.Update(findUser);
                UserModel userModel = _mapper.Map<User, UserModel>(findUser);
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }
        public async Task<UserResponseModel> FinalRemoval(Guid id)
        {
            UserResponseModel userResponseModel = await ValidateGetById(id);
            if (userResponseModel.Status)
            {
                User findUser = await _userRepository.GetById(id);
                await _userRepository.Delete(findUser);
                UserModel userModel = _mapper.Map<User, UserModel>(findUser);
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }
    }
}