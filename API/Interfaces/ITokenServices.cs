using API.Enitites;

namespace API.Interfaces
{
    public interface ITokenServices
    {
         string CreateToken(AppUser user);
    }
}
