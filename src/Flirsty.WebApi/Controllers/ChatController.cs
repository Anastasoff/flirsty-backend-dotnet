using Flirsty.Domain.Entities;
using Flirsty.Domain.Repositories;
using Flirsty.Domain.Services;
using Flirsty.WebApi.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Flirsty.WebApi.Controllers
{
    [RoutePrefix("Chat")]
    public class ChatController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenManager _tokenManager;

        public ChatController(IUserService userService, IUserRepository userRepository, ITokenManager tokenManager)
        {
            _userService = userService;
            _userRepository = userRepository;
            _tokenManager = tokenManager;
        }

        // GET api/Chat/Random
        [HttpGet]
        [Route("Random")]
        public async Task<IHttpActionResult> Random()
        {
            Guid userId = _tokenManager.GetUserIdFromToken(Request.Headers.Authorization.Parameter);

            if (string.IsNullOrEmpty(Request.Headers.Authorization?.Parameter))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            try
            {
                User user = await _userRepository.FindByIdAsync(userId);

                if (user.LookingFor.HasValue == false)
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Not specified LookingFor gender." };
                    throw new HttpResponseException(msg);
                }

                User randomUser = await _userService.GetOneRandomUser(x => x.Gender == user.LookingFor);

                return Ok(randomUser?.PublicInfo);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}