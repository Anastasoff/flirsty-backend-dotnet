using Flirsty.DataAccess.Repositories;
using Flirsty.Domain.Entities;
using Flirsty.Domain.Repositories;
using Flirsty.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Flirsty.Utilities;

namespace Flirsty.Service
{
    public class UserService : IUserService
    {
        private static IRandomGenerator _randomGenerator;
        private readonly IUserRepository _userRepository;

        public UserService(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
            _userRepository = new UserRepository();
        }

        public async Task<User> GetOneRandomUser(Expression<Func<User, bool>> filter)
        {
            int usersCount = await _userRepository.Count(filter);
            int randomNumber = _randomGenerator.NextInt(0, usersCount - 1);

            List<User> result = await _userRepository.Find(filter, 1, randomNumber);

            return result.SingleOrDefault();
        }
    }
}