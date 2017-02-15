using Flirsty.DataAccess.Repositories;
using Flirsty.DataAccess.Test.Helpers;
using Flirsty.Domain.Entities;
using Flirsty.Domain.Entities.Enums;
using Flirsty.Domain.Entities.ValueObjects;
using Flirsty.Domain.Repositories;
using Flirsty.Domain.Services;
using Flirsty.Service;
using Flirsty.Utilities;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Flirsty.DataAccess.Test
{
    public class ReturnsZeroRandomGeneratorMock : IRandomGenerator
    {
        public int NextInt(int min, int max)
        {
            return 0;
        }
    }

    public class ReturnsOneRandomGeneratorMock : IRandomGenerator
    {
        public int NextInt(int min, int max)
        {
            return 1;
        }
    }

    public class RandomUserTests
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IInterestRepository _interestRepository;

        public RandomUserTests()
        {
            _userService = new UserService(new RandomGenerator());
            _userRepository = new UserRepository();
            _interestRepository = new InterestRepository();
        }

        [Fact]
        public async Task FindOneRandomUser()
        {
            var interest = new Interest
            {
                Name = StringGenerator.GenerateRandomString(10)
            };

            await _interestRepository.AddAsync(interest);
            interest.Id.Should().NotBeEmpty();

            var publicInfo1 = new PublicInfo
            {
                NickName = StringGenerator.GenerateNameWithTimeStamp("user"),
                PictureUrl = StringGenerator.GenerateRandomString(50)
            };

            var user1 = new User
            {
                Email = StringGenerator.GenerateEmailWithTimeStamp("rnd_user", "mail.com"),
                Gender = Gender.Female,
                LookingFor = Gender.Male,
                PublicInfo = publicInfo1
            };

            await _userRepository.AddAsync(user1);
            user1.Id.Should().NotBeEmpty();

            var publicInfo2 = new PublicInfo
            {
                NickName = StringGenerator.GenerateNameWithTimeStamp("user"),
                PictureUrl = StringGenerator.GenerateRandomString(50)
            };

            var user2 = new User
            {
                Email = StringGenerator.GenerateEmailWithTimeStamp("rnd_user", "mail.com"),
                Gender = Gender.Male,
                LookingFor = Gender.Female,
                PublicInfo = publicInfo2
            };

            await _userRepository.AddAsync(user2);
            user2.Id.Should().NotBeEmpty();

            var userService1 = new UserService(new ReturnsZeroRandomGeneratorMock());
            User rndUser1 = await userService1.GetOneRandomUser(x => x.Gender == Gender.Female);
            Assert.NotNull(rndUser1);

            var userService2 = new UserService(new ReturnsOneRandomGeneratorMock());
            User rndUser2 = await userService2.GetOneRandomUser(x => x.Gender == Gender.Female);
            Assert.NotNull(rndUser2);

            Assert.NotEqual(rndUser1.Id, rndUser2.Id);
        }
    }
}