using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;

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
        public List<User> GetAllIsDeleted()
        {
            var allIsDeleted = _userRepository.GetAllIsDeleted();
            return allIsDeleted;
        }
        public List<User> GetAll()
        {
            var all = _userRepository.GetAll();
            return all;
        }
        public void Create(CreateModel createModel)
        {
            User user = new User();
            user = _mapper.Map<UserModel, User>(createModel);
            user.CreateDateTime = DateTime.Now;
            user.UpdateDateTime = user.CreateDateTime;
            _userRepository.Create(user);
        }
        public void Update(EditModel editModel)
        {
            var all = _userRepository.GetAll();
            var findUser = all.Find(x => x.Id == editModel.Id);
            findUser = _mapper.Map<UserModel, User>(editModel);
            findUser.UpdateDateTime = DateTime.Now;
            _userRepository.Update(findUser);
        }
        public void Delete(DeleteModel deleteModel)
        {
            var all = _userRepository.GetAll();
            var findUser = all.Find(x => x.Id == deleteModel.Id);
            findUser.IsDeleted = true;
            _userRepository.Update(findUser);
        }
        public void FinalRemoval(DeleteModel deleteModel)
        {
            var allFinalRemoval = _userRepository.GetAllIsDeleted();
            var findUser = allFinalRemoval.Find(x => x.Id == deleteModel.Id);
            _userRepository.Delete(findUser);
        }
    }
}
