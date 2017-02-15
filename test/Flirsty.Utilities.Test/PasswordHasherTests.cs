using System.Collections.Generic;
using Xunit;

namespace Flirsty.Utilities.Test
{
    public class PasswordHasherTests
    {
        [Fact]
        public void Should_generate_hashed_password_and_then_validate_it()
        {
            var passwords = new List<string>
            {
                "123456",
                "password",
                "~!@#$%^&*()_+?><|",
                "JHJH0u38*&*(hhfdf(*)("
            };

            foreach (var password in passwords)
            {
                string hashedPassword = PasswordHasher.HashPassword(password);
                bool isValidPassword = PasswordHasher.ValidatePassword(password, hashedPassword);
                Assert.True(isValidPassword);
            }
        }
    }
}