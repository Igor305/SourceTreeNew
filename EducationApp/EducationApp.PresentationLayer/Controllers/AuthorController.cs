using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    /// <summary>
    /// AuthorController
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorService"></param>
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
        [HttpGet("GetAll")]
        public async Task<AuthorResponseModel> GetAll()
        {
            AuthorResponseModel authorResponseModel = await _authorService.GetAll();
            return authorResponseModel;
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
        [HttpGet("GetAllWithoutRemove")]
        public async Task<AuthorResponseModel> GetAllWithoutRemove()
        {
            AuthorResponseModel authorResponseModel = await _authorService.GetAllWithoutRemove();
            return authorResponseModel;
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
        public async Task <AuthorResponseModel> Pagination([FromQuery] PaginationAuthorModel paginationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await _authorService.Pagination(paginationAuthorModel);
            return authorResponseModel;
        }
        /// <summary>
        /// Get by Id Author
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///
        ///     Get/GetById
        ///     {
        ///        "Id":"1a99d355-922a-42b1-51b8-08d759984c87"
        ///     }
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<AuthorResponseModel> GetById(Guid id)
        {
            AuthorResponseModel authorResponseModel = await _authorService.GetById(id);
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
        ///        "FirstName": "Сергей",
        ///        "LastName": "Конушенко"
        ///     }
        ///
        /// </remarks>
        [HttpGet("FindName")]
        public async Task<AuthorResponseModel> GetByFullName([FromQuery] GetNameAuthorModel getNameAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await _authorService.GetByFullName(getNameAuthorModel);
            return authorResponseModel;
        }
        /// <summary>
        /// Filtr Author
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///
        ///     Get/Filtr
        ///     {
        ///        "FirstName": "Сергей",
        ///        "LastName": "Конушенко"
        ///     }
        ///
        /// </remarks>
        [HttpGet("Filtration")]
        public AuthorResponseModel Filtration([FromQuery] FiltrationAuthorModel filtrationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = _authorService.Filtration(filtrationAuthorModel);
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
        ///        "FirstName": "Сергей",
        ///        "LastName": "Конушенко",
        ///        "DateBirth":"1805-10-09T08:38:40.163Z",
        ///        "DatadDeath": "1855-10-09T08:38:40.163Z",
        ///     }
        ///
        /// </remarks>
        [Produces("application/json")]
        [HttpPost("Create")]
        public async Task<AuthorResponseModel> Create([FromBody]CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await _authorService.Create(createAuthorModel);
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
        ///         "Id": "1d2267b6-b099-4139-0236-08d75c6d0bb9",
        ///         "FirstName": "Сергей",
        ///         "LastName": "Конушенко",
        ///         "DateBirth":"1805-10-09T08:38:40.163Z",
        ///         "DatadDeath": "1855-10-09T08:38:40.163Z",
        ///     }
        ///
        /// </remarks>
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<AuthorResponseModel> Update(Guid id, [FromBody]CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await _authorService.Update(id, createAuthorModel);
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
        ///         "Id": "1d2267b6-b099-4139-0236-08d75c6d0bb9"
        ///     }
        ///
        /// </remarks>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<AuthorResponseModel> Delete(Guid id)
        {
            AuthorResponseModel authorResponseModel = await _authorService.Delete(id);
            return authorResponseModel;
        }
    }
}

