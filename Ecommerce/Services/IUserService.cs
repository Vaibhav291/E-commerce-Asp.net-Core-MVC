using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterViewModel model);
        Task<User> AuthenticateAsync(LoginViewModel model);
    }
}
