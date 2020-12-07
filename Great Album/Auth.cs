using System.Web.Security;
using Users.BLL;
namespace Great_Album
{
    public static class Auth
    {
        public static bool CanLogin(string login, string password)
        {
            //UsersManager myStorage = new UsersManager();
            //return myStorage.CheckLoginAndPass(login, password);
            return true;
        }
    }
}