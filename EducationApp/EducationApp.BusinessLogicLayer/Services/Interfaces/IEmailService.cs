using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string body);
    }
}
