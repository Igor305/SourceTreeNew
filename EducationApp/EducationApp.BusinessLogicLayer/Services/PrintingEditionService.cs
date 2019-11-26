using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionsRepository;
        private readonly IMapper _mapper;
        public PrintingEditionService(IPrintingEditionRepository printingEditionsRepository, IMapper mapper)
        {
            _printingEditionsRepository = printingEditionsRepository;
            _mapper = mapper;
        }

        public async Task<PrintingEditionResponseModel> GetAll()
        {
            PrintingEditionResponseModel printingEditionResponseModel = ValidateOfSuccessfully();

            List<PrintingEdition> allIsDeleted = await _printingEditionsRepository.GetAll();
            List<PrintingEditionModel> printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(allIsDeleted);
            printingEditionResponseModel.PrintingEditionModel = printingEditionModel;

            return printingEditionResponseModel;
        }

        public async Task<PrintingEditionResponseModel> GetAllWithoutRemove()
        {
            PrintingEditionResponseModel printingEditionResponseModel = ValidateOfSuccessfully();

            List<PrintingEdition> all = await _printingEditionsRepository.GetAllWithoutRemove();
            List<PrintingEditionModel> printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(all);
            printingEditionResponseModel.PrintingEditionModel = printingEditionModel;

            return printingEditionResponseModel;
        }
        private PrintingEditionResponseModel ValidateOfSuccessfully()
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();

            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.Message = ResponseConstants.Successfully;

            return printingEditionResponseModel;
        }

        public async Task<PrintingEditionResponseModel> Pagination(PaginationPrintingEditionModel paginationPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = ValidatePagination(paginationPrintingEditionModel);

            List<PrintingEdition> printingEditions = await _printingEditionsRepository.Pagination(paginationPrintingEditionModel.Skip,  paginationPrintingEditionModel.Take);
            List<PrintingEditionModel> printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(printingEditions);
            printingEditionResponseModel.PrintingEditionModel = printingEditionModel;

            return printingEditionResponseModel;
        }

        private PrintingEditionResponseModel ValidatePagination(PaginationPrintingEditionModel paginationPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();

            bool isWarning = paginationPrintingEditionModel.Skip < 0 || paginationPrintingEditionModel.Take < 0;

            if (isWarning)
            {
                printingEditionResponseModel.Warning.Add(ResponseConstants.LessThanZero);
            }
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.Message = ResponseConstants.Successfully;

            return printingEditionResponseModel;
        }

        public async Task<PrintingEditionResponseModel> GetById(Guid id)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await ValidateGetById(id);

            if (printingEditionResponseModel.Status)
            {
                PrintingEdition findPrintingEdition = await _printingEditionsRepository.GetById(id);
                PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(findPrintingEdition);
                printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            }

            return printingEditionResponseModel;
        }

        private async Task<PrintingEditionResponseModel> ValidateGetById(Guid id)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();

            bool isExist = await _printingEditionsRepository.CheckById(id);

            if (!isExist)
            {
                printingEditionResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            printingEditionResponseModel.Status = isExist;
            printingEditionResponseModel.Message = printingEditionResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return printingEditionResponseModel;
        }

        public async Task<PrintingEditionResponseModel> Buy(Guid id)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await GetById(id);

            if (printingEditionResponseModel.Status)
            {
                PrintingEdition findbuyPrintingEdition = await _printingEditionsRepository.GetById(id);
                PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(findbuyPrintingEdition);
                printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            }
            return printingEditionResponseModel;
        }
        public async Task<PrintingEditionResponseModel> Sort(SortPrintingEditionModel sortPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = ValidateOfSuccessfully();
            List<PrintingEditionModel> printingEditionModel = new List<PrintingEditionModel>();

            switch (sortPrintingEditionModel.NameSort)
            {
                case PrintingEditionNameSort.None:
                    List<PrintingEdition> all = await _printingEditionsRepository.GetAll();
                    printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(all);
                    break;
                case PrintingEditionNameSort.Id:
                    List<PrintingEdition> sortId = _printingEditionsRepository.SortById();
                    printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(sortId);
                    break;
                case PrintingEditionNameSort.Name:
                    List<PrintingEdition> sortName = _printingEditionsRepository.SortByName();
                    printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(sortName);
                    break;
                case PrintingEditionNameSort.Price:
                    List<PrintingEdition> sortPrice = _printingEditionsRepository.SortByPrice();
                    printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(sortPrice);
                    break;
            }
            printingEditionResponseModel.PrintingEditionModel = printingEditionModel;

            return printingEditionResponseModel;
        }

        public async Task<PrintingEditionResponseModel> Filtration(FiltrationPrintingEditionModel filtrationPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = ValidateFiltration(filtrationPrintingEditionModel);

            if (printingEditionResponseModel.Status)
            {
                List<PrintingEdition> printingEditions = await _printingEditionsRepository.Filtration(filtrationPrintingEditionModel.Name, filtrationPrintingEditionModel.Price, filtrationPrintingEditionModel.Status);   
                List<PrintingEditionModel> printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(printingEditions);
                printingEditionResponseModel.PrintingEditionModel = printingEditionModel;
            }
            return printingEditionResponseModel;
        }

        private PrintingEditionResponseModel ValidateFiltration(FiltrationPrintingEditionModel filtrationPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();

            bool isWarningOfNull = filtrationPrintingEditionModel.Name == null || filtrationPrintingEditionModel.Price == 0 || filtrationPrintingEditionModel.Status == 0;

            if (isWarningOfNull)
            {
                printingEditionResponseModel.Warning.Add(ResponseConstants.Null);
            }
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.Message = ResponseConstants.Successfully;

            return printingEditionResponseModel;
        }

        public async Task<PrintingEditionResponseModel> Create(CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = ValidateCreate(createPrintingEditionModel);
 
            if (printingEditionResponseModel.Status)
            {
                PrintingEdition printingEdition = _mapper.Map<CreatePrintingEditionModel, PrintingEdition>(createPrintingEditionModel);
                printingEdition.CreateDateTime = DateTime.Now;
                printingEdition.UpdateDateTime = DateTime.Now;
                await _printingEditionsRepository.Create(printingEdition);
                PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(printingEdition);
                printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            }
            return printingEditionResponseModel;
        }

        private PrintingEditionResponseModel ValidateCreate(CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();

            bool isErrorOfNull = string.IsNullOrEmpty(createPrintingEditionModel.Name) || createPrintingEditionModel.Price == 0 || string.IsNullOrEmpty(createPrintingEditionModel.Type);
            bool isErrorLessThanZero = createPrintingEditionModel.Price < 0;

            bool isError = isErrorOfNull || isErrorLessThanZero;

            bool isWarningOfNull = createPrintingEditionModel.Description == null;

            if (isErrorOfNull)
            {
                printingEditionResponseModel.Error.Add(ResponseConstants.Null);
            }
            if (isErrorLessThanZero)
            {
                printingEditionResponseModel.Error.Add(ResponseConstants.LessThanZero);
            }

            if (isWarningOfNull)
            {
                printingEditionResponseModel.Error.Add(ResponseConstants.Null);
            }
            printingEditionResponseModel.Status = !isError;
            printingEditionResponseModel.Message = printingEditionResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return printingEditionResponseModel;
        }

        public async Task<PrintingEditionResponseModel> Update(Guid id, CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await ValidateUpdate(id, createPrintingEditionModel);
            if (printingEditionResponseModel.Status)
            {
                PrintingEdition PrintingEdition = await _printingEditionsRepository.GetById(id);
                _mapper.Map(createPrintingEditionModel, PrintingEdition);
                PrintingEdition.UpdateDateTime = DateTime.Now;
                await _printingEditionsRepository.Update(PrintingEdition);
                PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(PrintingEdition);
                printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            }
            return printingEditionResponseModel;
        }

        private async Task<PrintingEditionResponseModel> ValidateUpdate(Guid id, CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await ValidateGetById(id); 
            printingEditionResponseModel = ValidateCreate(createPrintingEditionModel);

            return printingEditionResponseModel;
        }

        public async Task<PrintingEditionResponseModel> Delete(Guid id)
        {
            PrintingEditionResponseModel printingEditionResponseModel = await ValidateGetById(id);

            if (printingEditionResponseModel.Status)
            {
                PrintingEdition findPrintingEdition = await _printingEditionsRepository.GetById(id);
                findPrintingEdition.IsDeleted = true;
                await _printingEditionsRepository.Update(findPrintingEdition);
                PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(findPrintingEdition);
                printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            }
            return printingEditionResponseModel;
        }
    }
}