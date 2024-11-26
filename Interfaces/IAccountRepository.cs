using KpiAlumni.Tables;

namespace KpiAlumni.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserProfile> GetByIdAsync(int id);
        Task<UserProfile> CreateAsync(UserProfile userModel);
        Task<bool> EmailExistsAsync(string email);
        Task<UserProfile> GetUserByUsernameAsync(string username);

    }
}
