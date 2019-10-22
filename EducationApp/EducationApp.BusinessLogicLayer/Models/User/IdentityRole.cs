using System;

namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class IdentityRole
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        public virtual string ConcurrencyStamp { get; set; }
    }
}
