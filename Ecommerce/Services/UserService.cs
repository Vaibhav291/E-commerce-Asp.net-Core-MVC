using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _hasher;

        public UserService(IUserRepository repository, IPasswordHasher<User> hasher)
        {
            _repository = repository;
            _hasher = hasher;
        }

        public async Task RegisterAsync(RegisterViewModel model)
        {
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Role = "User"
            };

            user.PasswordHash = _hasher.HashPassword(user, model.Password);
            await _repository.AddAsync(user);
        }

        public async Task<User> AuthenticateAsync(LoginViewModel model)
        {
            var user = await _repository.GetByEmailAsync(model.Email);

            if(user == null)
            {
                return null;
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
