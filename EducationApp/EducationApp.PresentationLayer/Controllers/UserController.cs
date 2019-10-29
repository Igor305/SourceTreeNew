using EducationApp.BusinessLogicLayer.Models.ResponseModels.User;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
        [HttpGet("GetAllIsDeleted")]
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
        [HttpGet("GetAll")]
        public async Task<UserResponseModel> GetAll()
        {
            UserResponseModel userResponseModel = await _userService.GetAll();
            return userResponseModel;
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
        [HttpPost("Create")]
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
        [HttpPut("Update")]
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
        [HttpDelete("Delete")]
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
        [HttpDelete("FinalRemoval")]
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
