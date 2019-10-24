using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<PrintingEditionResponseModel> GetAllIsDeleted()
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            List<PrintingEdition> allIsDeleted = await _printingEditionsRepository.GetAllIsDeleted();
            List<PrintingEditionModel> printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(allIsDeleted);
            printingEditionResponseModel.Messege = "Successfully";
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.PrintingEditionModel = printingEditionModel;
            return printingEditionResponseModel;
        }
        public async Task<PrintingEditionResponseModel> GetAll()
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            List<PrintingEdition> all = await _printingEditionsRepository.GetAll();
            List<PrintingEditionModel> printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(all);
            printingEditionResponseModel.Messege = "Successfully";
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.PrintingEditionModel = printingEditionModel;
            return printingEditionResponseModel;
        }
        public PrintingEditionResponseModel Pagination(PaginationPagePrintingEditionModel paginationPagePrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            if (paginationPagePrintingEditionModel.Skip < 0)
            {
                printingEditionResponseModel.Messege = "Error";
                printingEditionResponseModel.Status = false;
                printingEditionResponseModel.Error.Add("Skip < 0");
            }
            if (paginationPagePrintingEditionModel.Take < 0)
            {
                printingEditionResponseModel.Messege = "Error";
                printingEditionResponseModel.Status = false;
                printingEditionResponseModel.Error.Add("Take < 0");
            }
            if (printingEditionResponseModel.Messege == null)
            {
                IQueryable<PrintingEdition> pagination = _printingEditionsRepository.Pagination();
                int count = pagination.Count();
                List<PrintingEdition> items = pagination.Skip(paginationPagePrintingEditionModel.Skip).Take(paginationPagePrintingEditionModel.Take).ToList();

                PaginationPrintingEditionModel paginationPrintingEditionModel = new PaginationPrintingEditionModel(count, paginationPagePrintingEditionModel.Skip, paginationPagePrintingEditionModel.Take);
                IndexViewModel viewModel = new IndexViewModel
                {
                    PaginationPrintingEditionModel = paginationPrintingEditionModel,
                    PrintingEditions = items
                };
                List<PrintingEditionModel> printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(items);
                printingEditionResponseModel.Messege = "Successfully";
                printingEditionResponseModel.Status = true;
                printingEditionResponseModel.PrintingEditionModel = printingEditionModel;
            }
            return printingEditionResponseModel;
        }
        public async Task<PrintingEditionResponseModel> Buy(BuyPrintingEditionModel buyPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            PrintingEdition findbuyPrintingEdition = await _printingEditionsRepository.GetById(buyPrintingEditionModel.Id);
            if (findbuyPrintingEdition == null)
            {
                printingEditionResponseModel.Messege = "Error";
                printingEditionResponseModel.Status = false;
                printingEditionResponseModel.Error.Add("No print with this id");
            }
            if (printingEditionResponseModel.Messege == null)
            {
                PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(findbuyPrintingEdition);
                printingEditionResponseModel.Messege = "Successfully";
                printingEditionResponseModel.Status = true;
                printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            }
            return printingEditionResponseModel;
        }
        public async Task<PrintingEditionResponseModel> Create(CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            PrintingEdition printingEdition = _mapper.Map<CreatePrintingEditionModel, PrintingEdition>(createPrintingEditionModel);
            printingEdition.CreateDateTime = DateTime.Now;
            printingEdition.UpdateDateTime = DateTime.Now;
            await _printingEditionsRepository.Create(printingEdition);
            PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(printingEdition);
            printingEditionResponseModel.Messege = "Successfully";
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            return printingEditionResponseModel;
        }
        public async Task<PrintingEditionResponseModel> Update(UpdatePrintingEditionModel updatePrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            PrintingEdition findPrintingEdition = await _printingEditionsRepository.GetById(updatePrintingEditionModel.Id);
            _mapper.Map(updatePrintingEditionModel, findPrintingEdition);
            findPrintingEdition.UpdateDateTime = DateTime.Now;
            await _printingEditionsRepository.Update(findPrintingEdition);
            PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(findPrintingEdition);
            printingEditionResponseModel.Messege = "Successfully";
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            return printingEditionResponseModel;
        }
        public async Task<PrintingEditionResponseModel> Delete(DeletePrintingEditionModel deletePrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            PrintingEdition findPrintingEdition = await _printingEditionsRepository.GetById(deletePrintingEditionModel.Id);
            findPrintingEdition.IsDeleted = true;
            await _printingEditionsRepository.Update(findPrintingEdition);
            PrintingEditionModel printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(findPrintingEdition);
            printingEditionResponseModel.Messege = "Successfully";
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.PrintingEditionModel.Add(printingEditionModel);
            return printingEditionResponseModel;
        }
        public async Task<PrintingEditionResponseModel> Sort(SortPrintingEditionModel sortPrintingEditionModel)
        {
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            List<PrintingEditionModel> printingEditionModel = new List<PrintingEditionModel>();
            List<PrintingEdition> all = await _printingEditionsRepository.GetAll();
            switch (sortPrintingEditionModel.NameSort)
            {
                case PrintingEditionNameSort.Id:
                    List<PrintingEdition> sortId = all.OrderBy(x => x.Id).ToList();
                    printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(sortId);
                    break;
                case PrintingEditionNameSort.Name:
                    List<PrintingEdition> sortName = all.OrderBy(x => x.Name).ToList();
                    printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(sortName);
                    break;
                case PrintingEditionNameSort.Price:
                    List<PrintingEdition> sortPrice = all.OrderBy(x => x.Price).ToList();
                    printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(sortPrice);
                    break;
                default:
                    printingEditionResponseModel.Messege = "Error";
                    printingEditionResponseModel.Status = false;
                    printingEditionResponseModel.Error.Add("Invalid sort type");
                    return printingEditionResponseModel;
            }
            printingEditionResponseModel.Messege = "Successfully";
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.PrintingEditionModel = printingEditionModel;
            return printingEditionResponseModel;
        }
        public async Task<PrintingEditionResponseModel> Filter(FiltrationPrintingEditionModel filtrationPrintingEditionModel)
        {
            List<PrintingEdition> all = await _printingEditionsRepository.GetAll();
            List<PrintingEdition> filter = all.Where(x => x.Name == filtrationPrintingEditionModel.Name).Where(x => x.Price == filtrationPrintingEditionModel.Price).Where(x => x.Status == filtrationPrintingEditionModel.Status).ToList();
            List<PrintingEditionModel> printingEditionModel = _mapper.Map<List<PrintingEdition>, List<PrintingEditionModel>>(filter);
            PrintingEditionResponseModel printingEditionResponseModel = new PrintingEditionResponseModel();
            printingEditionResponseModel.Messege = "Successfully";
            printingEditionResponseModel.Status = true;
            printingEditionResponseModel.PrintingEditionModel = printingEditionModel;
            return printingEditionResponseModel;
        }
    }
}
