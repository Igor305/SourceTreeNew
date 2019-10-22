using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public object GetAllIsDeleted()
        {
            if (ModelState.IsValid)
            {
                var allIsDeleted = _authorService.GetAllIsDeleted();
                return allIsDeleted;
            }
            return "Запись не валидна(";
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
        public object GetAll()
        {
            if (ModelState.IsValid)
            {
                var all = _authorService.GetAll();
                return all;
            }
            return "Запись не валидна(";
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
        public object Pagination([FromQuery] PaginationPageAuthorModel paginationPageAuthorModel)
        {
            if (ModelState.IsValid)
            {
                var all = _authorService.Pagination(paginationPageAuthorModel);
                return all;
            }
            return "Запись не валидна(";
        }
        /// <summary>
        /// Get Name Author
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///
        ///     Get/Find Name
        ///     {
        ///        "Name": "Пушкин"
        ///     }
        ///
        /// </remarks>
        [HttpGet("FindName")]
        public object FindName([FromQuery] GetNameAuthorModel getNameAuthorModel)
        {
            if (ModelState.IsValid)
            {
                var all = _authorService.FindName(getNameAuthorModel);
                return all;
            }
            return "Запись не валидна(";
        }
        /// <summary>
        /// Create new Author
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/Create
        ///     {
        ///        "Name": "Пушкин",
        ///        "DateBirth":"1805-10-09T08:38:40.163Z",
        ///        "DatadDeath": "1855-10-09T08:38:40.163Z",
        ///     }
        ///
        /// </remarks>
        [Produces("application/json")]
        [HttpPost("Create")]
        public string Create([FromBody]CreateAuthorModel createAuthorModel)
        {
            if (ModelState.IsValid)
            {
                string result = _authorService.Create(createAuthorModel);
                return result;
            }
            return "Запись не валидна(";
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
        ///         "Name": "Пушкин",
        ///         "DateBirth":"1805-10-09T08:38:40.163Z",
        ///         "DatadDeath": "1855-10-09T08:38:40.163Z",
        ///     }
        ///
        /// </remarks>
        [Produces("application/json")]
        [HttpPut("Update")]
        public string Update([FromBody]UpdateAuthorModel updateAuthorModel)
        {
            if (ModelState.IsValid)
            {
                var result = _authorService.Update(updateAuthorModel);
                return result;
            }
            return "Запись не валидна(";
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
        public string Delete([FromBody]DeleteAuthorModel deleteAuthorModel)
        {
            if (ModelState.IsValid)
            {
                _authorService.Delete(deleteAuthorModel);
                return "Запись удалена";
            }
            return "Запись не валидна(";
        }
    }
}

