using Microsoft.IdentityModel.Tokens;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IJwtRefresh
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
