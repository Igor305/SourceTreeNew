using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.BusinessLogicLayer.Models.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.User;
using EducationApp.BusinessLogicLayer.Models.User;

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
            RoleAccountResponseModel roleAccountResponseModel = _accountService.GetAllRoleUsers();
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
        public async Task<UserResponseModel> GetAllIsDeleted()
        {
            UserResponseModel userResponseModel = await _userService.GetAllIsDeleted();
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
        public async Task<UserResponseModel> GetAll()
        {
            UserResponseModel userResponseModel = await _userService.GetAll();
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
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            if (ModelState.IsValid)
            {
                roleAccountResponseModel = await _accountService.CreateRoleUser(createRoleModel);
                return roleAccountResponseModel;
            }
            roleAccountResponseModel.Messege = "Error";
            roleAccountResponseModel.Status = false;
            roleAccountResponseModel.Error.Add("Not IsValid");
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
            UserResponseModel userResponseModel = new UserResponseModel();
            if (ModelState.IsValid)
            {
                userResponseModel = await _userService.Create(createmodel);
                return userResponseModel;
            }
            userResponseModel.Messege = "Error";
            userResponseModel.Status = false;
            userResponseModel.Error.Add("Post, not valide");
            return userResponseModel;
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
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            if (ModelState.IsValid)
            {
                roleAccountResponseModel = await _accountService.UpdateRoleUser(updateRoleModel);
                return roleAccountResponseModel;
            }
            roleAccountResponseModel.Messege = "Error";
            roleAccountResponseModel.Status = false;
            roleAccountResponseModel.Error.Add("Not IsValid");
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
        [HttpPut("UpdateUser")]
        public async Task<UserResponseModel> Update([FromBody]UpdateUserModel model)
        {
            UserResponseModel userResponseModel = new UserResponseModel();
            if (ModelState.IsValid)
            {
                userResponseModel = await _userService.Update(model);
                return userResponseModel;
            }
            userResponseModel.Messege = "Error";
            userResponseModel.Status = false;
            userResponseModel.Error.Add("Post, not valide");
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
            RoleAccountResponseModel roleAccountResponseModel = new RoleAccountResponseModel();
            if (ModelState.IsValid)
            {
                roleAccountResponseModel = await _accountService.DeleteRoleUser(deleteRoleModel);
                return roleAccountResponseModel;
            }
            roleAccountResponseModel.Messege = "Error";
            roleAccountResponseModel.Status = false;
            roleAccountResponseModel.Error.Add("Not IsValid");
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
        public async Task<UserResponseModel> Delete([FromBody]DeleteModel deleteModel)
        {
            UserResponseModel userResponseModel = new UserResponseModel();
            if (ModelState.IsValid)
            {
                userResponseModel = await _userService.Delete(deleteModel);
                return userResponseModel;
            }
            userResponseModel.Messege = "Error";
            userResponseModel.Status = false;
            userResponseModel.Error.Add("Post, not valide");
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
        [HttpDelete("FinalRemovalUser")]
        public async Task<UserResponseModel> FinalRemoval([FromBody]DeleteModel deleteModel)
        {
            UserResponseModel userResponseModel = new UserResponseModel();
            if (ModelState.IsValid)
            {
                userResponseModel = await _userService.FinalRemoval(deleteModel);
                return userResponseModel;
            }
            userResponseModel.Messege = "Error";
            userResponseModel.Status = false;
            userResponseModel.Error.Add("Post, not valide");
            return userResponseModel;
        }
    }
}