using finWise.Model;

namespace finWise.Interfaces
{
    public interface IUserInterface
    {
        Task<UserDetails> RegisterAsync(UserDetails users);

        Task<UserDetails> LoginAsync(string userid, string password);
        Task<UserDetails> ResetPasswordAsync(string userEmail, string newPassword);
        Task<bool> DeleteAsync(string emailId);
        Task<UserDetails> GetProfileAsync(string userId);
    }
}
