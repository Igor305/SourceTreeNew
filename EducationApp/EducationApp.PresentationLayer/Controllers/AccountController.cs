using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.BusinessLogicLayer.Models.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.User;
using EducationApp.BusinessLogicLayer.Models.User;
using System;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.UserInRole;
using Microsoft.AspNetCore.Authorization;

namespace EducationApp.PresentationLayer.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="userService"></param>
        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }
        /// <summary>
        /// Get All Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAllRole
        ///
        /// </remarks>
        [HttpGet("GetAllRole")]
        public RoleAccountResponseModel GetAllRoleUsers()
        {
            RoleAccountResponseModel roleAccountResponseModel = _accountService.GetAllRoles();
            return roleAccountResponseModel;
        }
        /// <summary>
        /// Get all User (IsDeleted = true)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAllIsDeleted
        ///
        /// </remarks>
        [HttpGet("GetAllIsDeletedUsers")]
        public async Task<UserResponseModel> GetAll()
        {
            UserResponseModel userResponseModel = await _userService.GetAll();
            return userResponseModel;
        }
        /// <summary>
        /// Get all User 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAll
        ///
        /// </remarks>
        [HttpGet("GetAllUsers")]
        public async Task<UserResponseModel> GetAllWithoutRemove()
        {
            UserResponseModel userResponseModel = await _userService.GetAllWithoutRemove();
            return userResponseModel;
        }
        /// <summary>
        ///  Create Roles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/CreateRoleUsers
        /// 
        ///     "Name":"Slave"
        ///     
        /// </remarks>
        [HttpPost("CreateRole")]
        public async Task<RoleAccountResponseModel> CreateRoleUsers([FromBody] CreateRoleModel createRoleModel)
        {
            RoleAccountResponseModel roleAccountResponseModel = await _accountService.CreateRole(createRoleModel);
            return roleAccountResponseModel;
        }
        /// <summary>
        /// Create new PrintingEdition
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Create
        ///     {
        ///         "Email": "karamba@gmail.com",
        ///         "FirstName": "Олег",
        ///         "LastName": "Петрович",
        ///         "PhoneNumber": "096453453455"
        ///     }
        ///
        /// </remarks>
        [HttpPost("CreateUser")]
        public async Task<UserResponseModel> Create([FromBody]CreateUserModel createmodel)
        {
            UserResponseModel userResponseModel = await _userService.Create(createmodel);
            return userResponseModel;
        }
        /// <summary>
        /// Adding Role User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Create
        ///     {
        ///         "Email": "karamba@gmail.com",
        ///         "NameRole": "Slave"
        ///     }
        ///
        /// </remarks>
        [HttpPost("AddingRoleUser")]
        public async Task<UserInRoleResponseModel> AddingRoleUser([FromBody]ChangeRoleUserModel changeRoleUserModel)
        {
            UserInRoleResponseModel userInRoleResponseModel = await _accountService.AddingRoleUser(changeRoleUserModel);
            return userInRoleResponseModel;
        }
        /// <summary>
        /// Taking Away User Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Create
        ///     {
        ///         "Email": "karamba@gmail.com",
        ///         "NameRole": "Slave"
        ///     }
        ///
        /// </remarks>
        [HttpPost("TakingAwayUserRole")]
        public async Task<UserInRoleResponseModel> TakingAwayUserRole([FromBody]ChangeRoleUserModel changeRoleUserModel)
        {
            UserInRoleResponseModel userInRoleResponseModel = await _accountService.TakingAwayUserRole(changeRoleUserModel);
            return userInRoleResponseModel;
        }
        /// <summary>
        ///  Update Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put/UpdateRoleUser
        /// 
        ///     "Name":"Slave"
        ///     
        /// </remarks>
        [HttpPut("UpdateRole")]
        public async Task<RoleAccountResponseModel> UpdateRoleUser([FromBody] UpdateRoleModel updateRoleModel)
        {
            RoleAccountResponseModel roleAccountResponseModel = await _accountService.UpdateRole(updateRoleModel);
            return roleAccountResponseModel;
        }
        /// <summary>
        /// Update User for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/Update
        ///     {
        ///         "Id": "90b75db0-1daf-4790-db0a-08d75c6f078d",
        ///         "Email": "karamba@gmail.com",
        ///         "FirstName": "Олег",
        ///         "LastName": "Петрович",
        ///         "PhoneNumber": "096453453455"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<UserResponseModel> Update(Guid id, [FromBody]CreateUserModel createUserModel)
        {
            UserResponseModel userResponseModel = await _userService.Update(id, createUserModel);
            return userResponseModel;
        }
        /// <summary>
        ///  DeleteRoleUsers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete/DeleteRoleUsers
        /// 
        ///     "Name":"Slave"
        ///     
        /// </remarks>
        [HttpDelete("DeleteRole")]
        public async Task<RoleAccountResponseModel> DeleteRoleUser([FromBody] DeleteRoleModel deleteRoleModel)
        {
            RoleAccountResponseModel roleAccountResponseModel = await _accountService.DeleteRole(deleteRoleModel);
            return roleAccountResponseModel;
        }
        /// <summary>
        /// Delete User for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/Delete
        ///     {
        ///         "Id": "90b75db0-1daf-4790-db0a-08d75c6f078d"
        ///     }
        ///
        /// </remarks>
        [HttpDelete("DeleteUser")]
        public async Task<UserResponseModel> Delete(Guid id)
        {
            UserResponseModel userResponseModel = await _userService.Delete(id);
            return userResponseModel;
        }
        /// <summary>
        /// Final Removal User for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/FinalRemoval
        ///     {
        ///         "Id": "90b75db0-1daf-4790-db0a-08d75c6f078d"
        ///     }
        ///
        /// </remarks>
        [HttpDelete("FinalRemovalUser/{id}")]
        public async Task<UserResponseModel> FinalRemoval(Guid id)
        {
            UserResponseModel userResponseModel  = await _userService.FinalRemoval(id);
            return userResponseModel;
        }
    }
}