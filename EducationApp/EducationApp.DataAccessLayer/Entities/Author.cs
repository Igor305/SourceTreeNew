using EducationApp.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Entities
{
    public class Author : Basic
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DataBirth { get; set; }
        public DateTime? DataDeath { get; set; }

        private ICollection<AuthorInPrintingEdition> _autorInPrintingEditions;
        public virtual ICollection<AuthorInPrintingEdition> AutorInPrintingEdition
        {
            get => this._autorInPrintingEditions ?? (this._autorInPrintingEditions = new HashSet<AuthorInPrintingEdition>());
            set => this._autorInPrintingEditions = value;
        }
 
    }
}
