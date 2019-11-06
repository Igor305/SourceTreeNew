using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Account
{
    public class RoleAccountModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
