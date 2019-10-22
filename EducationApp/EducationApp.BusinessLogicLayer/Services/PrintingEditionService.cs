using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<PrintingEdition> GetAllIsDeleted()
        {
            var allIsDeleted = _printingEditionsRepository.GetAllIsDeleted();
            return allIsDeleted;
        }
        public List<PrintingEdition> GetAll()
        {
            var all = _printingEditionsRepository.GetAll();
            return all;
        }
        public List<PrintingEdition> Pagination(PaginationPagePrintingEditionModel paginationPagePrintingEditionModel)
        {
            if (paginationPagePrintingEditionModel.Skip < 1)
            {
                paginationPagePrintingEditionModel.Skip = 1;
            }
            var pagination = _printingEditionsRepository.Pagination();
            var count = pagination.Count();
            var items = pagination.Skip(paginationPagePrintingEditionModel.Skip).Take(paginationPagePrintingEditionModel.Take).ToList();

            PaginationPrintingEditionModel paginationPrintingEditionModel = new PaginationPrintingEditionModel(count, paginationPagePrintingEditionModel.Skip, paginationPagePrintingEditionModel.Take);
            IndexViewModel viewModel = new IndexViewModel
            {
                PaginationPrintingEditionModel = paginationPrintingEditionModel,
                PrintingEditions = items
            };
            return items;
        }
        public object Buy(BuyPrintingEditionModel buyPrintingEditionModel)
        {
            var all = _printingEditionsRepository.GetAll();
            var findbuyPrintingEdition = all.Find(x => x.Id == buyPrintingEditionModel.Id);
            if (findbuyPrintingEdition == null)
            {
                return "Нету печатного издания с таким Id";
            }
            return findbuyPrintingEdition;
        }
        public void Create(CreatePrintingEditionModel createPrintingEditionModel)
        {
            PrintingEdition printingEdition = _mapper.Map<PrintingEditionModel, PrintingEdition>(createPrintingEditionModel);
            printingEdition.CreateDateTime = DateTime.Now;
            printingEdition.UpdateDateTime = DateTime.Now;
            _printingEditionsRepository.Create(printingEdition);
        }
        public void Update(UpdatePrintingEditionModel updatePrintingEditionModel)
        {
            PrintingEdition findPrintingEdition = _printingEditionsRepository.GetById(updatePrintingEditionModel.Id);
            _mapper.Map(updatePrintingEditionModel, findPrintingEdition);
            findPrintingEdition.UpdateDateTime = DateTime.Now;
            _printingEditionsRepository.Update(findPrintingEdition);
        }
        public void Delete(DeletePrintingEditionModel deletePrintingEditionModel)
        {
            PrintingEdition findPrintingEdition = _printingEditionsRepository.GetById(deletePrintingEditionModel.Id);
            findPrintingEdition.IsDeleted = true;
            _printingEditionsRepository.Update(findPrintingEdition);
        }
        public object Sort(SortPrintingEditionModel sortPrintingEditionModel)
        {
            var all = _printingEditionsRepository.GetAll();
            switch (sortPrintingEditionModel.NameSort)
            {
                case PrintingEditionNameSort.Id:
                    var sortId = all.OrderBy(x => x.Id);
                    return sortId;
                case PrintingEditionNameSort.Name:
                    var sortName = all.OrderBy(x => x.Name);
                    return sortName;
                case PrintingEditionNameSort.Price:
                    var sortPrice = all.OrderBy(x => x.Price);
                    return sortPrice;
                default:
                    return "Чёт не так)";
            }

        }
        public object Filter(FiltrationPrintingEditionModel filtrationPrintingEditionModel)
        {
            var all = _printingEditionsRepository.GetAll();
            var filterName = all.Where(x => x.Name == filtrationPrintingEditionModel.Name);
            var filterPrice = all.Where(x => x.Price == filtrationPrintingEditionModel.Price);
            var filterStatus = all.Where(x => x.Status == filtrationPrintingEditionModel.Status);
            return filterStatus;
        }
    }
}
