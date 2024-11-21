using KpiAlumni.Data;
using KpiAlumni.Interfaces;
using KpiAlumni.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KpiAlumni.Repository
{
    public class AccountRepository :IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var acct=await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return acct;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }
    }
}
