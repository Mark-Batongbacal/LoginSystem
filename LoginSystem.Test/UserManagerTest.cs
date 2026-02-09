using FluentAssertions;
using LoginSystem.Models.Database;
using LoginSystem.Repository.Users;
using LoginSystem.Services.Users;
using Moq;
using Xunit;

namespace LoginSystem.Test
{
    public class UserManagerTest
    {
        private readonly Mock<IUserDataManager> _repo;
        private readonly UserManager _userManager;

        public UserManagerTest()
        {
            _repo = new Mock<IUserDataManager>();
            _userManager = new UserManager(_repo.Object);
        }

        private static User CreateValidUser() => new()
        {
            UserName = "testsdjafh",
            Email = "tesasasasddasdddta@gmail.com",
            Password = "Pasasdsword"
        };

        private static UserDetail CreateValidUserDetail() => new()
        {


            FirstName = "Testasd",
            LastName = "Testerasd",
            Address = "Test Addrsadess",
            Age = 25,
            Gender = "M"
        };

        [Fact]
        public async Task CreateUserAsync_CreatesUser_WhenEmailIsUnique()
        {
            _repo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);


            var user = CreateValidUser();

            var userDetails = CreateValidUserDetail();

            await _userManager.CreateUserAsync(user, userDetails);
            _repo.Verify(r => r.AddUserAsync(user, userDetails), Times.Once);
            user.IsActive.Should().BeTrue();
        }

        [Fact]
        public async Task CreateUserAsync_Throws_WhenEmailAlreadyExists()
        {
            _repo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());

            var user = CreateValidUser();
            var userDetails = CreateValidUserDetail();

            Func<Task> act = () =>
                _userManager.CreateUserAsync(CreateValidUser(), CreateValidUserDetail());

            await act.Should().ThrowAsync<Exception>();

            _repo.Verify(x => x.AddUserAsync(It.IsAny<User>(), It.IsAny<UserDetail>()), Times.Never);
        }


        [Fact]
        public async Task GetUserAsync_ReturnsNull_WhenUserNotFound()
        {
            _repo.Setup(r => r.GetFullUserAsync(It.IsAny<int>())).ReturnsAsync((User?)null);
            var result = await _userManager.GetUserAsync(7);
            result.Should().BeNull(); 
        }
    }
}
