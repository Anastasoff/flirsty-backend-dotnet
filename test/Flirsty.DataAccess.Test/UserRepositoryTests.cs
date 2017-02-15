using Flirsty.DataAccess.Repositories;
using Flirsty.DataAccess.Test.Helpers;
using Flirsty.Domain.Entities;
using Flirsty.Domain.Repositories;
using System;
using System.Threading.Tasks;
using Flirsty.Domain.Entities.Enums;
using Flirsty.Domain.Entities.ValueObjects;
using Xunit;

namespace Flirsty.DataAccess.Test
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            _userRepository = new UserRepository();
        }

        [Fact]
        public async Task Should_create_new_user_and_then_find_it_by_Id()
        {
            // Arrange
            var expectedUser = new User();

            // Act
            await _userRepository.AddAsync(expectedUser);
            Guid expectedUserId = expectedUser.Id;

            var actualUser = await _userRepository.FindByIdAsync(expectedUserId);

            Assert.NotNull(actualUser);
            Assert.Equal(expectedUser.Id, actualUser.Id);
            Assert.Equal(expectedUserId, actualUser.Id);
        }

        [Fact]
        public async Task Should_find_user_by_email()
        {
            // Arrange
            string expectedEmail = StringGenerator.GenerateEmailWithTimeStamp("test", "mail.com");
            var expectedUser = new User { Email = expectedEmail };

            // Act
            await _userRepository.AddAsync(expectedUser);
            var actualUser = await _userRepository.FindByEmailAsync(expectedEmail);

            Assert.NotNull(actualUser);
            Assert.Equal(expectedEmail, actualUser.Email);
        }

        [Fact]
        public async Task Should_save_all_user_info()
        {
            User friend = new User
            {
                Email = StringGenerator.GenerateEmailWithTimeStamp("test", "mail.com"),
                Gender = Gender.Female,
                PublicInfo = new PublicInfo
                {
                    NickName = StringGenerator.GenerateNameWithTimeStamp("test"),
                    PictureUrl = StringGenerator.GenerateRandomString(30)
                }
            };

            await _userRepository.AddAsync(friend);
            Assert.NotEqual(friend.Id, Guid.Empty);

            User user = new User
            {
                Email = StringGenerator.GenerateEmailWithTimeStamp("test", "mail.com"),
                Gender = Gender.Male,
                PublicInfo = new PublicInfo
                {
                    NickName = StringGenerator.GenerateNameWithTimeStamp("test"),
                    PictureUrl = StringGenerator.GenerateRandomString(30)
                }
            };
            user.FriendsIds.Add(friend.Id);

            await _userRepository.AddAsync(user);
            Assert.NotEqual(user.Id, Guid.Empty);
        }
    }
}