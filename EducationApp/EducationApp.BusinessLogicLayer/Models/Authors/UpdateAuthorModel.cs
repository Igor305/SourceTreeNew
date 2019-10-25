using EducationApp.BusinessLogicLayer.Models.Base;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class UpdateAuthorModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DataBirth { get; set; }
        public DateTime? DataDeath { get; set; }
    }
}
