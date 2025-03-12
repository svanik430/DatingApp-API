using API.DTOs;
using API.Enitites;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAsyncAll();
        
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser?> GetUsersByIdAsync(int id);
        Task<AppUser?> GetUsersByNameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembesrAsync();
        Task<MemberDto?> GetMemberAsync(string username);

    }
}
