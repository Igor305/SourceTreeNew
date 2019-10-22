using System.ComponentModel.DataAnnotations;

namespace EducationApp.BusinessLogicLayer.Models.Authors
{
    public class GetNameAuthorModel
    {
        [Required]
        public string Name { get; set; }
    }
}
