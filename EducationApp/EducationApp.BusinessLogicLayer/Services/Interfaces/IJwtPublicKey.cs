using Microsoft.IdentityModel.Tokens;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IJwtPublicKey
    {
        SecurityKey GetKey();
    }
}
