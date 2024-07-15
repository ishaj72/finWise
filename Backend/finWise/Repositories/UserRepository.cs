using finWise.Interfaces;
using finWise.Model;
using Microsoft.EntityFrameworkCore;

namespace finWise.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly finWiseDbContext _context;
        //private readonly IEmailSenderInterface _emailSender;

        public UserRepository(finWiseDbContext context)
        {
            _context = context;
            //_emailSender = emailSender;
        }

        public async Task<UserDetails> RegisterAsync(UserDetails users)
        {
            // Ensure unique UserId
            if (await _context.Users.AnyAsync(u => u.UserId == users.UserId))
            {
                throw new InvalidOperationException("User ID already exists. Please enter any different id");
            }

            if (users.Age < 18)
            {
                throw new InvalidOperationException("Age should not be less than 18.");
            }
            // Assign default role if not provided
            if (string.IsNullOrEmpty(users.Role))
            {
                users.Role = "User"; // Assign a default role
            }

            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            return users;
        }

        public async Task<UserDetails> LoginAsync(string userid, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userid && u.Password == password);
        }


        public async Task<bool> DeleteAsync(string emailId)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == emailId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<UserDetails> ResetPasswordAsync(string userEmail, string newPassword)
        {
            var changePassword = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == userEmail);
            if (changePassword != null)
            {
                changePassword.Password = newPassword;
                await _context.SaveChangesAsync();
                return changePassword;
            }
            return null;
        }

        public async Task<UserDetails> GetProfileAsync(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

    }
}
