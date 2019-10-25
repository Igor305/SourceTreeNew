using System;

namespace EducationApp.DataAccessLayer.Entities.Base
{
    public class Basic
    {
        public Guid Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
