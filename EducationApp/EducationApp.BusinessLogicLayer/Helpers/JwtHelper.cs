using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Helpers
{
    public class JwtHelper : IJwtPrivateKey
    {
        private readonly SymmetricSecurityKey _accesstoken;
        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

        public JwtHelper(string keyaccesstoken)
        {
            this._accesstoken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyaccesstoken));
        }
        public SecurityKey GetKey() => this._accesstoken;
    }
}
