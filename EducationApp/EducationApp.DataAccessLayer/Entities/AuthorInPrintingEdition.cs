using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace EducationApp.DataAccessLayer.Entities
{
    public class AuthorInPrintingEdition
    {
        private Author _author;
        private PrintingEdition _printingEdition;
        private readonly ILazyLoader _lazyLoader;
        public AuthorInPrintingEdition()
        {
        }
        private AuthorInPrintingEdition(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public Guid AutorId { get; set; }
        public virtual Author Autor { get; set; }
        public Guid PrintingEditionId { get; set; }
        public virtual PrintingEdition PrintingEdition { get; set; }
    }
}

