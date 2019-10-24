using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        /// <summary>
        /// Get all Author (IsDeleted = true)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAllIsDeleted
        ///
        /// </remarks>
        [HttpGet("GetAllIsDeleted")]
        public async Task<AuthorResponseModel> GetAllIsDeleted()
        {
            AuthorResponseModel allIsDeleted = await _authorService.GetAllIsDeleted();
            return allIsDeleted;
        }
        /// <summary>
        /// Get All Author
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAll
        ///
        /// </remarks>
        [HttpGet("GetAll")]
        public async Task<AuthorResponseModel> GetAll()
        {
            AuthorResponseModel all = await _authorService.GetAll();
            return all;
        }
        /// <summary>
        /// Get Pagination Author
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///
        ///     Get/Pagination
        ///     {
        ///        "Skip": "1",
        ///        "Take":"2"
        ///     }
        ///
        /// </remarks>
        [HttpGet("Pagination")]
        public AuthorResponseModel Pagination([FromQuery] PaginationPageAuthorModel paginationPageAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if (ModelState.IsValid)
            {
                authorResponseModel = _authorService.Pagination(paginationPageAuthorModel);
                return authorResponseModel;
            }
            authorResponseModel.Messege = "Error";
            authorResponseModel.Status = false;
            authorResponseModel.Error.Add("Post, not valide");
            return authorResponseModel;
        }
        /// <summary>
        /// Get Name Author
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///
        ///     Get/Find Name
        ///     {
        ///        "First Name": "Сергей",
        ///        "Last Name": "Конушенко"
        ///     }
        ///
        /// </remarks>
        [HttpGet("FindName")]
        public async Task<AuthorResponseModel> FindName([FromQuery] GetNameAuthorModel getNameAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if (ModelState.IsValid)
            {
                authorResponseModel = await _authorService.FindName(getNameAuthorModel);
                return authorResponseModel;
            }
            authorResponseModel.Messege = "Error";
            authorResponseModel.Status = false;
            authorResponseModel.Error.Add("Post, not valide");
            return authorResponseModel;
        }
        /// <summary>
        /// Create new Author
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/Create
        ///     {
        ///        "First Name": "Сергей",
        ///        "Last Name": "Конушенко",
        ///        "DataBirth":"1805-10-09T08:38:40.163Z",
        ///        "DatadDeath": "1855-10-09T08:38:40.163Z",
        ///     }
        ///
        /// </remarks>
        [Produces("application/json")]
        [HttpPost("Create")]
        public async Task<AuthorResponseModel> Create([FromBody]CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if (ModelState.IsValid)
            {
                authorResponseModel = await _authorService.Create(createAuthorModel);
                return authorResponseModel;
            }
            authorResponseModel.Messege = "Error";
            authorResponseModel.Status = false;
            authorResponseModel.Error.Add("Post, not valide");
            return authorResponseModel;
        }
        /// <summary>
        /// Update Author for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/Update
        ///     {
        ///         "Id": "",
        ///         "First Name": "Сергей",
        ///         "Last Name": "Конушенко",
        ///         "DataBirth":"1805-10-09T08:38:40.163Z",
        ///         "DatadDeath": "1855-10-09T08:38:40.163Z",
        ///     }
        ///
        /// </remarks>
        [Produces("application/json")]
        [HttpPut("Update")]
        public async Task<AuthorResponseModel> Update([FromBody]UpdateAuthorModel updateAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if (ModelState.IsValid)
            {
                authorResponseModel = await _authorService.Update(updateAuthorModel);
                return authorResponseModel;
            }
            authorResponseModel.Messege = "Post, not valide";
            return authorResponseModel;
        }
        /// <summary>
        /// Delete  Author for Id
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
        [Produces("application/json")]
        [HttpDelete("Delete")]
        public async Task<AuthorResponseModel> Delete([FromBody]DeleteAuthorModel deleteAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if (ModelState.IsValid)
            {
                authorResponseModel = await _authorService.Delete(deleteAuthorModel);
                return authorResponseModel;
            }
            authorResponseModel.Messege = "Post, not valide";
            return authorResponseModel;
        }
    }
}

