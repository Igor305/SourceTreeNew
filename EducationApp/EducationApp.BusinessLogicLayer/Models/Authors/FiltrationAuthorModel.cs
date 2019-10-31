using EducationApp.BusinessLogicLayer.Models.Enums;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class FiltrationAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateBirthFirst { get; set; }
        public DateTime? DateBirthLast { get; set; }
        public DateTime? DateDeathFirst { get; set; }
        public DateTime? DateDeathLast { get; set; }
    }
}
