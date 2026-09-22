using School.Domain.Entities;

namespace School.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByIdAsync(int id);

    Task<User> AddAsync(User user);

    Task<bool> EmailExistsAsync(string email);
    Task UpdateAsync(User user);
}