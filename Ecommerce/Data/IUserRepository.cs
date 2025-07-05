﻿using Ecommerce.Models;

namespace Ecommerce.Data
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
