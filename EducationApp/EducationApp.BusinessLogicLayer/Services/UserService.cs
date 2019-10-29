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
            UserResponseModel userResponseModel = new UserResponseModel();
            if (createUserModel.Email == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("Email not null");
            }
            if (createUserModel.FirstName == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("FirstName not null");
            }
            if (createUserModel.LastName == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("LastName not null");
            }
            if (createUserModel.PhoneNumber == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("PhoneNumber not null");
            }
            if (userResponseModel.Messege == null)
            {
                User user = _mapper.Map<CreateUserModel, User>(createUserModel);
                user.CreateDateTime = DateTime.Now;
                user.UpdateDateTime = user.CreateDateTime;
                await _userRepository.Create(user);
                UserModel userModel = _mapper.Map<User, UserModel>(user);
                userResponseModel.Messege = "Successfully";
                userResponseModel.Status = true;
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }
        public async Task<UserResponseModel> Update(UpdateUserModel updateUserModel)
        {
            UserResponseModel userResponseModel = new UserResponseModel();
            User findUser = await _userRepository.GetById(updateUserModel.Id);
            if (findUser == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("This Id is not in database");
            }
            if (updateUserModel.Email == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("Email not null");
            }
            if (updateUserModel.FirstName == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("FirstName not null");
            }
            if (updateUserModel.LastName == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("LastName not null");
            }
            if (updateUserModel.PhoneNumber == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("PhoneNumber not null");
            }
            if (userResponseModel.Messege == null)
            {
                _mapper.Map(updateUserModel,findUser);
                findUser.UpdateDateTime = DateTime.Now;
                await _userRepository.Update(findUser);
                UserModel userModel = _mapper.Map<User, UserModel>(findUser);
                userResponseModel.Messege = "Successfully";
                userResponseModel.Status = true;
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }
        public async Task<UserResponseModel> Delete(DeleteModel deleteModel)
        {
            UserResponseModel userResponseModel = new UserResponseModel();
            User findUser = await _userRepository.GetById(deleteModel.Id);
            if (findUser == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("This Id is not in database");
            }
            if (userResponseModel.Messege == null)
            {
                findUser.IsDeleted = true;
                await _userRepository.Update(findUser);
                UserModel userModel = _mapper.Map<User, UserModel>(findUser);
                userResponseModel.Messege = "Successfully";
                userResponseModel.Status = true;
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }
        public async Task<UserResponseModel> FinalRemoval(DeleteModel deleteModel)
        {
            UserResponseModel userResponseModel = new UserResponseModel();
            User findUser = await _userRepository.GetByIdAllIsDeleted(deleteModel.Id);
            if (findUser == null)
            {
                userResponseModel.Messege = "Error";
                userResponseModel.Status = false;
                userResponseModel.Error.Add("This Id is not in database");
            }
            if (userResponseModel.Messege == null)
            {
                await _userRepository.Delete(findUser);
                UserModel userModel = _mapper.Map<User, UserModel>(findUser);
                userResponseModel.Messege = "Successfully";
                userResponseModel.Status = true;
                userResponseModel.UserModels.Add(userModel);
            }
            return userResponseModel;
        }
    }
}
