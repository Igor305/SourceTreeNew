using EducationApp.BusinessLogicLayer.Models.Base;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class GetAllAuthorModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DataDeath { get; set; }
    }
}
