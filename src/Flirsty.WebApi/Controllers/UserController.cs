using Flirsty.DataAccess.Repositories;
using Flirsty.Domain.Entities;
using Flirsty.Domain.Repositories;
using Flirsty.WebApi.Models;
using Flirsty.WebApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Flirsty.WebApi.Controllers
{
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenManager _tokenManager;

        public UserController(IUserRepository userRepository, ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _tokenManager = tokenManager;
        }

        // GET api/User/Current
        [HttpGet]
        [Route("Current")]
        public async Task<IHttpActionResult> Current()
        {
            Guid userId = _tokenManager.GetUserIdFromToken(Request.Headers.Authorization.Parameter);

            if (string.IsNullOrEmpty(Request.Headers.Authorization?.Parameter))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            try
            {
                User user = await _userRepository.FindByIdAsync(userId);
                List<User> friends = await _userRepository.FindUsersByIds(user.FriendsIds);

                UserResponseModel model = new UserResponseModel
                {
                    ServerId = user.Id.ToString(),
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    Gender = user.Gender,
                    LookingFor = user.LookingFor,
                    PublicInfo = user.PublicInfo,
                    Location = user.Location,
                    Seconds = user.Seconds,
                    FriendsList = friends.Select(x => new UserResponseModel
                    {
                        ServerId = x.Id.ToString(),
                        Email = x.Email,
                        BirthDate = x.BirthDate,
                        Gender = x.Gender,
                        LookingFor = x.LookingFor,
                        PublicInfo = x.PublicInfo,
                        Location = x.Location,
                        Seconds = x.Seconds
                    }).ToList()
                };

                return Ok(model);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // POST api/User/Edit
        [HttpPost]
        [Route("Edit")]
        public async Task<IHttpActionResult> Edit([FromBody] UserRequestModel model)
        {
            Guid userId = _tokenManager.GetUserIdFromToken(Request.Headers.Authorization.Parameter);

            if (string.IsNullOrEmpty(Request.Headers.Authorization?.Parameter))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (model == null)
            {
                return BadRequest("Invalid user model");
            }

            try
            {
                User user = await _userRepository.FindByIdAsync(userId);

                if (user == null)
                {
                    return BadRequest($"User with email: {model.Email} was not found!");
                }

                user.Email = model.Email;
                user.PublicInfo = model.PublicInfo;
                user.BirthDate = model.BirthDate;
                user.Gender = model.Gender;
                user.LookingFor = model.LookingFor;
                user.Seconds = model.Seconds;

                await _userRepository.UpdateAsync(user);

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}