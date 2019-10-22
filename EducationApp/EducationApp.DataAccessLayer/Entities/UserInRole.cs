using System;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.DataAccessLayer.Entities
{
    public class UserInRole
    {
        [Key]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

