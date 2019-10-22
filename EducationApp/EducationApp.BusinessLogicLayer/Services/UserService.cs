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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            User user = new User
            {
                Email = createModel.Email,
                UserName = createModel.Email,
                FirstName = createModel.FirstName,
                LastName = createModel.LastName,
                PhoneNumber = createModel.PhoneNumber,
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };
            _userRepository.Create(user);
        }
        public void Update(EditModel editModel)
        {
            var all = _userRepository.GetAll();
            var findUser = all.Find(x => x.Id == editModel.Id);
            findUser.Email = editModel.Email;
            findUser.UserName = editModel.Email;
            findUser.FirstName = editModel.FirstName;
            findUser.LastName = editModel.LastName;
            findUser.PhoneNumber = editModel.PhoneNumber;
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
