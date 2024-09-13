using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.IdentityModel.Tokens.Jwt;


namespace STS.DataTransferObjects.Helpers
{
    public class JwtHelpers
    {
        private IJsonSerializer _serializer;
        private IDateTimeProvider _provider;
        private IJwtValidator _validator;
        private IBase64UrlEncoder _urlEncoder;
        private IJwtAlgorithm _algorithm;
        private IJwtDecoder _decoder;

        public JwtHelpers()
        {
            _serializer = new JsonNetSerializer();
            _provider = new UtcDateTimeProvider();
            _validator = new JwtValidator(_serializer, _provider);
            _urlEncoder = new JwtBase64UrlEncoder();
            _algorithm = new HMACSHA256Algorithm();
            _decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
        }

        public JwtSecurityToken DecodeJWTToken(string accessToken)
        {
            try
            {
                if (!String.IsNullOrEmpty(accessToken))
                {
                    var handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken token = handler.ReadJwtToken(accessToken);
                    return token;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
