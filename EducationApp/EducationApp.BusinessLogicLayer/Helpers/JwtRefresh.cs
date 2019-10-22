using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Helpers
{
    public class JwtRefresh : IJwtRefresh
    {
        private readonly SymmetricSecurityKey _refreshtoken;
        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

        public JwtRefresh(string keyrefreshtoken)
        {
            this._refreshtoken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyrefreshtoken));
        }
        public SecurityKey GetKey() => this._refreshtoken;
    }
}
