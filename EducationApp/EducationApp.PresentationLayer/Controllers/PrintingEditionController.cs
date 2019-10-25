using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        private readonly IPrintingEditionService _printingEditionService;
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
        public async Task<PrintingEditionResponseModel> GetAllIsDeleted()
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.GetAllIsDeleted();
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
        public async Task <PrintingEditionResponseModel> GetAll()
        {
            PrintingEditionResponseModel printingEditionResponseModel = await _printingEditionService.GetAll();
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
        public PrintingEditionResponseModel Pagination([FromQuery] PaginationPagePrintingEditionModel paginationPagePrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            if (ModelState.IsValid)
            {
                printingEditionResponseModel = _printingEditionService.Pagination(paginationPagePrintingEditionModel);
                return printingEditionResponseModel;
            }
            printingEditionResponseModel.Messege = "Error";
            printingEditionResponseModel.Status = false;
            printingEditionResponseModel.Error.Add("Post, not valide");
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
        ///         "NameSort": ""
        ///     }
        ///
        /// </remarks>
        [HttpGet("Sort")]
        public async Task<PrintingEditionResponseModel> Sort([FromQuery]SortPrintingEditionModel sortPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            if (ModelState.IsValid)
            {
                printingEditionResponseModel = await _printingEditionService.Sort(sortPrintingEditionModel);
                return printingEditionResponseModel;
            }
            printingEditionResponseModel.Messege = "Error";
            printingEditionResponseModel.Status = false;
            printingEditionResponseModel.Error.Add("Post, not valide");
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
        ///         "NameFilter": "",
        ///         "Name":"",
        ///         "Price":"",
        ///         "Status":"",
        ///     }
        ///
        /// </remarks>
        [HttpGet("Filter")]
        public async Task<PrintingEditionResponseModel> Filter([FromQuery]FiltrationPrintingEditionModel filtrationPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            if (ModelState.IsValid)
            {
                printingEditionResponseModel = await _printingEditionService.Filter(filtrationPrintingEditionModel);
                return printingEditionResponseModel;
            }
            printingEditionResponseModel.Messege = "Error";
            printingEditionResponseModel.Status = false;
            printingEditionResponseModel.Error.Add("Post, not valide");
            return printingEditionResponseModel;
        }
        /// <summary>
        /// Buy PrintingEdition
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Buy
        ///     {
        ///         "Id": "",
        ///     }
        ///
        /// </remarks>
        [HttpGet("Buy")]
        public async Task<PrintingEditionResponseModel> Buy([FromQuery]BuyPrintingEditionModel buyPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            if (ModelState.IsValid)
            {
                printingEditionResponseModel = await _printingEditionService.Buy(buyPrintingEditionModel);
                return printingEditionResponseModel;
            }
            printingEditionResponseModel.Messege = "Error";
            printingEditionResponseModel.Status = false;
            printingEditionResponseModel.Error.Add("Post, not valide");
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
        ///         "Name": "",
        ///         "Description": "",
        ///         "Price": "",
        ///         "Type": "",
        ///         "Status": "",
        ///         "Currency": ""
        ///     }
        ///
        /// </remarks>
        [HttpPost("Create")]
        public async Task<PrintingEditionResponseModel> Create([FromBody]CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            if (ModelState.IsValid)
            {
                printingEditionResponseModel = await _printingEditionService.Create(createPrintingEditionModel);
                return printingEditionResponseModel;
            }
            printingEditionResponseModel.Messege = "Error";
            printingEditionResponseModel.Status = false;
            printingEditionResponseModel.Error.Add("Post, not valide");
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
        ///         "Id": "",
        ///         "Name": "",
        ///         "Description": "",
        ///         "Price": "",
        ///         "Type": "",
        ///         "Status": "",
        ///         "Currency": ""
        ///     }
        ///
        /// </remarks>
        [HttpPut("Update")]
        public async Task<PrintingEditionResponseModel> Update([FromBody]UpdatePrintingEditionModel updatePrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            if (ModelState.IsValid)
            {
                printingEditionResponseModel = await _printingEditionService.Update(updatePrintingEditionModel);
                return printingEditionResponseModel;
            }
            printingEditionResponseModel.Messege = "Error";
            printingEditionResponseModel.Status = false;
            printingEditionResponseModel.Error.Add("Post, not valide");
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
        ///         "Id": ""
        ///     }
        ///
        /// </remarks>
        [HttpDelete("Delete")]
        public async Task<PrintingEditionResponseModel> Delete([FromBody]DeletePrintingEditionModel deletePrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            if (ModelState.IsValid)
            {
                printingEditionResponseModel = await _printingEditionService.Delete(deletePrintingEditionModel);
                return printingEditionResponseModel;
            }
            printingEditionResponseModel.Messege = "Error";
            printingEditionResponseModel.Status = false;
            printingEditionResponseModel.Error.Add("Post, not valide");
            return printingEditionResponseModel;
        }
    }
}
