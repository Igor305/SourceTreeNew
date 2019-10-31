using EducationApp.BusinessLogicLayer.Models.Enums;
using System;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class FiltrationAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateBirthFrom { get; set; }
        public DateTime? DateBirthTo { get; set; }
        public DateTime? DateDeathFrom { get; set; }
        public DateTime? DateDeathTo { get; set; }
    }
}
