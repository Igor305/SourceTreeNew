using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public object GetAllIsDeleted()
        {
            var all = _userService.GetAllIsDeleted();
            return all;
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
        public object GetAll()
        {
            var all = _userService.GetAll();
            return all;
        }
        /// <summary>
        /// Create new PrintingEdition
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Create
        ///     {
        ///         "Email": "",
        ///         "First Name": "",
        ///         "Last Name": "",
        ///         "PhoneNumber": ""
        ///     }
        ///
        /// </remarks>
        [HttpPost("Create")]
        public string Create([FromBody]CreateModel createmodel)
        {
            if (ModelState.IsValid)
            {
                _userService.Create(createmodel);
                return "Данные внесены ";
            }
            return "Что-то не так";
        }
        /// <summary>
        /// Update User for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/Update
        ///     {
        ///         "Id": "",
        ///         "Email": "",
        ///         "First Name": "",
        ///         "Last Name": "",
        ///         "PhoneNumber": ""
        ///     }
        ///
        /// </remarks>
        [HttpPut("Update")]
        public string Update([FromBody]EditModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.Update(model);
                return "Данные изменены ";
            }
            return "Что-то не так";
        }
        /// <summary>
        /// Delete User for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/Delete
        ///     {
        ///         "Id": ""
        ///     }
        ///
        /// </remarks>
        [HttpDelete("Delete")]
        public string Delete([FromBody]DeleteModel deleteModel)
        {
            _userService.Delete(deleteModel);
            return "Пользователь под номером " + deleteModel.Id + " был удачно удалён";
        }
        /// <summary>
        /// Final Removal User for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/FinalRemoval
        ///     {
        ///         "Id": ""
        ///     }
        ///
        /// </remarks>
        [HttpDelete("FinalRemoval")]
        public string FinalRemoval([FromBody]DeleteModel deleteModel)
        {
            _userService.FinalRemoval(deleteModel);
            return "Пользователь под номером " + deleteModel.Id + " был окончательно удалён";
        }
    }
}
