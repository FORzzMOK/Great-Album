using Entities;
using Users.DAL;

namespace Common
{
    public class Dependensies
    {
        public static IUserStorable UsersStorage { get; } = new UserMemoryStorage();
    }
}
