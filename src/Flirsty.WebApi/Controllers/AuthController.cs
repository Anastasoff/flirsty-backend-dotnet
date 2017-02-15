using Flirsty.Domain.Entities;
using Flirsty.Domain.Entities.Enums;
using Flirsty.Domain.Entities.ValueObjects;
using Flirsty.Domain.Repositories;
using Flirsty.WebApi.Models;
using Flirsty.WebApi.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Flirsty.WebApi.Controllers
{
    [RoutePrefix("Auth")]
    public class AuthController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IResponseValuesGetter _responseValuesGetter;

        public AuthController(IUserRepository userRepository, ITokenManager tokenManager, IResponseValuesGetter responseValuesGetter)
        {
            _userRepository = userRepository;
            _tokenManager = tokenManager;
            _responseValuesGetter = responseValuesGetter;
        }

        // POST api/auth/google
        [HttpPost]
        [Route("Google")]
        public async Task<IHttpActionResult> Google([FromBody] GoogleRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid || model == null)
                {
                    return BadRequest();
                }

                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", model.Code),
                    new KeyValuePair<string, string>("redirect_uri", "http://localhost/signin-google"),
                    new KeyValuePair<string, string>("client_id", model.ClientId)
                };

                var client = new HttpClient();
                var url = new Uri(Constants.TokenEndpoint);
                var form = new FormUrlEncodedContent(body);
                var tokenResponse = await client.PostAsync(url, form);

                if (tokenResponse.IsSuccessStatusCode == false)
                {
                    return BadRequest();
                }

                string text = await tokenResponse.Content.ReadAsStringAsync();
                JObject response = JObject.Parse(text);
                string accessToken = response.Value<string>("access_token");
                string tokenType = response.Value<string>("token_type");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return NotFound();
                }

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Constants.UserInfoEndpoint);
                request.Headers.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                HttpResponseMessage userInfoResponse = await client.SendAsync(request);

                if (userInfoResponse.IsSuccessStatusCode == false)
                {
                    return BadRequest();
                }

                text = await userInfoResponse.Content.ReadAsStringAsync();

                JObject userObj = JObject.Parse(text);

                string userEmail = _responseValuesGetter.TryGetValue(userObj, "email");
                string googleId = _responseValuesGetter.TryGetValue(userObj, "id");

                User user = await _userRepository.FindByEmailAsync(userEmail);

                if (user == null)
                {
                    var info = new PublicInfo
                    {
                        NickName = _responseValuesGetter.TryGetValue(userObj, "name"),
                        PictureUrl = _responseValuesGetter.TryGetValue(userObj, "picture")
                    };

                    user = new User
                    {
                        Email = userEmail,
                        Gender = _responseValuesGetter.GetGender(_responseValuesGetter.TryGetValue(userObj, "gender")),
                        PublicInfo = info
                    };

                    user.ExternalKeys.Add(new ExternalKey
                    {
                        Key = googleId,
                        Type = ExternalSystemType.Google
                    });

                    await _userRepository.AddAsync(user);
                }

                List<User> userFriends = await _userRepository.FindUsersByIds(user.FriendsIds);

                string sendToken = _tokenManager.CreateSendToken(user);

                var responseModel = new GoogleResponseModel
                {
                    User = new UserResponseModel
                    {
                        ServerId = user.Id.ToString(),
                        Email = user.Email,
                        PublicInfo = user.PublicInfo,
                        BirthDate = user.BirthDate,
                        Gender = user.Gender,
                        LookingFor = user.LookingFor,
                        Seconds = user.Seconds,
                        FriendsList = userFriends.Select(x => new UserResponseModel
                        {
                            ServerId = x.Id.ToString(),
                            Email = x.Email,
                            PublicInfo = x.PublicInfo,
                            BirthDate = x.BirthDate,
                            Gender = x.Gender,
                            LookingFor = x.LookingFor,
                            Seconds = x.Seconds,
                        }).ToList()
                    },
                    Token = sendToken
                };
                return Ok(responseModel);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}