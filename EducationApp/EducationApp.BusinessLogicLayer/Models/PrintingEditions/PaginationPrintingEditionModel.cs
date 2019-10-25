using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEditions
{
    public class PaginationPrintingEditionModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PaginationPrintingEditionModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
    public class IndexViewModel
    {
        public IEnumerable<PrintingEdition> PrintingEditions { get; set; }
        public PaginationPrintingEditionModel PaginationPrintingEditionModel { get; set; }
    }
}
