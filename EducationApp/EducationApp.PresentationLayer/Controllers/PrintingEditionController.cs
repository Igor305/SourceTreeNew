using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    /// <summary>
    /// PrintingEditionController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        private readonly IPrintingEditionService _printingEditionService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printingEditionService"></param>
        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }
        /// <summary>
        /// Get all PrintingEdition (IsDeleted = true)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAllIsDeleted
        ///
        /// </remarks>
        [HttpGet("GetAllIsDeleted")]
        public async Task<PrintingEditionResponseModel> GetAll()
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.GetAll();
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Get all PrintingEdition 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAll
        ///
        /// </remarks>
        [HttpGet("GetAll")]
        public async Task <PrintingEditionResponseModel> GetAllWithoutRemove()
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.GetAllWithoutRemove();
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Get Pagination PrintingEdition
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
        public async Task<PrintingEditionResponseModel> Pagination([FromQuery] PaginationPrintingEditionModel paginationPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel  = await _printingEditionService.Pagination(paginationPrintingEditionModel);
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Get by Id PrintingEdition
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetById
        ///     {
        ///         "Id": "1d2267b6-b099-4139-0236-08d75c6d0bb9",
        ///     }
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<PrintingEditionResponseModel> GetById(Guid id)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.GetById(id);
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Buy PrintingEdition
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/Buy
        ///     {
        ///         "Id": "1d2267b6-b099-4139-0236-08d75c6d0bb9",
        ///     }
        ///
        /// </remarks>
        [HttpGet("Buy/{id}")]
        public async Task<PrintingEditionResponseModel> Buy(Guid id)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.Buy(id);
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Sort Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/Sort
        ///     {
        ///         "NameSort": "1"
        ///     }
        ///
        /// </remarks>
        [HttpGet("Sort")]
        public async Task<PrintingEditionResponseModel> Sort([FromQuery]SortPrintingEditionModel sortPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.Sort(sortPrintingEditionModel);
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Filter Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/Filter
        ///     {
        ///         "Name":"Козак",
        ///         "Price":"15000",
        ///         "Status":"1",
        ///     }
        ///
        /// </remarks>
        [HttpGet("Filter")]
        public async Task<PrintingEditionResponseModel> Filter([FromQuery]FiltrationPrintingEditionModel filtrationPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.Filter(filtrationPrintingEditionModel);
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Create new PrintingEdition
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Create
        ///     {
        ///         "Name": "Козак",
        ///         "Description": "Терпи козак, атаманом станешь",
        ///         "Price": "15000",
        ///         "Type": "Книга",
        ///         "Status": "1",
        ///         "Currency": "56"
        ///     }
        ///
        /// </remarks>
        [HttpPost("Create")]
        public async Task<PrintingEditionResponseModel> Create([FromBody]CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.Create(createPrintingEditionModel);
            return printingEditionResponseModel;

        }
        /// <summary>
        /// Update PrintingEdition for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/Update
        ///     {
        ///         "Id": "1d2267b6-b099-4139-0236-08d75c6d0bb9",
        ///         "Name": "Козак",
        ///         "Description": "Терпи козак, атаманом станешь",
        ///         "Price": "15000",
        ///         "Type": "Книга",
        ///         "Status": "1",
        ///         "Currency": "56"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<PrintingEditionResponseModel> Update(Guid id, [FromBody]CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.Update(id, createPrintingEditionModel);
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Delete PrintingEdition for Id
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
        [HttpDelete("{id}")]
        public async Task<PrintingEditionResponseModel> Delete(Guid id)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.Delete(id);
            return printingEditionResponseModel;
        }
    }
}
