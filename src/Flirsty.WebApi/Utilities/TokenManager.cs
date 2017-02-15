using Flirsty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Flirsty.WebApi.Utilities
{
    public interface ITokenManager
    {
        Guid GetUserIdFromToken(string token);

        string CreateSendToken(User user);
    }

    public class TokenManager : ITokenManager
    {
        public Guid GetUserIdFromToken(string token)
        {
            try
            {
                var payload = (IDictionary<string, object>)JWT.JsonWebToken.DecodeToObject(token, Constants.SecretKey);
                return Guid.Parse((string)payload["sub"]);
            }
            catch (Exception)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid token!" };
                throw new HttpResponseException(msg);
            }
        }

        public string CreateSendToken(User user)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var now = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds);
            var payload = new Dictionary<string, object>
            {
                { "sub", user.Id },
                { "exp", now + TimeSpan.FromDays(10).TotalSeconds }
            };

            string token = JWT.JsonWebToken.Encode(payload, Constants.SecretKey, JWT.JwtHashAlgorithm.HS256);

            return token;
        }
    }
}