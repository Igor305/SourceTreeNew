using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public object GetAllIsDeleted()
        {
            if (ModelState.IsValid)
            {
                var getAll = _printingEditionService.GetAllIsDeleted();
                return getAll;
            }
            return "Модель не валидная(";
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
        public object GetAll()
        {
            if (ModelState.IsValid)
            {
                var getAll = _printingEditionService.GetAll();
                return getAll;
            }
            return "Модель не валидная(";
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
        public object Pagination([FromQuery] PaginationPagePrintingEditionModel paginationPagePrintingEditionModel)
        {
            if (ModelState.IsValid)
            {
                var filter = _printingEditionService.Pagination(paginationPagePrintingEditionModel);
                return filter;
            }
            return "Модель не валидная(";
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
        [HttpPost("Buy")]
        public object Buy([FromBody]BuyPrintingEditionModel buyPrintingEditionModel)
        {
            if (ModelState.IsValid)
            {
                var buy = _printingEditionService.Buy(buyPrintingEditionModel);
                return buy;
            }
            return "Модель не валидная";
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
        public string Create([FromBody]CreatePrintingEditionModel createPrintingEditionModel)
        {
            if (ModelState.IsValid)
            {
                _printingEditionService.Create(createPrintingEditionModel);
                return "Добавлена новая запись";
            }
            return "Модель не валидная(";
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
        public string Update([FromBody]UpdatePrintingEditionModel updatePrintingEditionModel)
        {
            if (ModelState.IsValid)
            {
                _printingEditionService.Update(updatePrintingEditionModel);
                return "Запись обновлена";
            }
            return "Запись не валидна(";
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
        public string Delete([FromBody]DeletePrintingEditionModel deletePrintingEditionModel)
        {
            if (ModelState.IsValid)
            {
                _printingEditionService.Delete(deletePrintingEditionModel);
                return "Запись удалена";
            }
            return "Запись не валидна(";
        }
        /// <summary>
        /// Sort Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Sort
        ///     {
        ///         "NameSort": ""
        ///     }
        ///
        /// </remarks>
        [HttpPost("Sort")]
        public object Sort([FromBody]SortPrintingEditionModel sortPrintingEditionModel)
        {
            if (ModelState.IsValid)
            {
                var sort = _printingEditionService.Sort(sortPrintingEditionModel);
                return sort;
            }
            return "Модель не валидная(";
        }
        /// <summary>
        /// Filter Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Filter
        ///     {
        ///         "NameFilter": "",
        ///         "Name":"",
        ///         "Price":"",
        ///         "Status":"",
        ///     }
        ///
        /// </remarks>
        [HttpPost("Filter")]
        public object Filter([FromBody]FiltrationPrintingEditionModel filtrationPrintingEditionModel)
        {
            if (ModelState.IsValid)
            {
                var filter = _printingEditionService.Filter(filtrationPrintingEditionModel);
                return filter;
            }
            return "Модель не валидная(";
        }
    }
}
