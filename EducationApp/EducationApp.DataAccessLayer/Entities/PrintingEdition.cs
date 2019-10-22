using EducationApp.DataAccessLayer.Entities.Base;
using EducationApp.DataAccessLayer.Entities.Enum;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Entities
{
    public class PrintingEdition : Basic
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public OrderItem OrderItem { get; set; }

        private ICollection<AuthorInPrintingEdition> _autorInPrintingEditions;
        public virtual ICollection<AuthorInPrintingEdition> AutorInPrintingEdition
        {
            get => this._autorInPrintingEditions ?? (this._autorInPrintingEditions = new HashSet<AuthorInPrintingEdition>());
            set => this._autorInPrintingEditions = value;
        }
    }
}

