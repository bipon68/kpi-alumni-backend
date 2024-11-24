using KpiAlumni.Tables;

namespace KpiAlumni.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User userModel);
        Task<bool> EmailExistsAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);

    }
}
