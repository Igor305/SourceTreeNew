using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors
{
    public class AuthorModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateBirth{ get; set; }
        public DateTime? DateDeath { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
