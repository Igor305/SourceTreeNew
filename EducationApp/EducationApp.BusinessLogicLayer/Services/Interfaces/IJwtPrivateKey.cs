using Microsoft.IdentityModel.Tokens;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IJwtPrivateKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
