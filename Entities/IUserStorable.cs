using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IUserStorable 
    {
        User GetUserByLogin(string login);
        bool CheckLoginAndPass(string login, string password);
        bool CheckLogin(string login);
        bool AddUser(User user);
        bool EditName(string login, string newName);
        bool EditSurname(string login, string newSurname);
        bool EditStatus(string login, string status);
        bool EditDateOfBirth(string login, DateTime newDateOfBirth);
        bool EditEmail(string login, string newEmail);
        bool EditAvatar(string login, byte[] newAvatar);
        bool AddPhoto(string login, byte[] newPhoto, string title);
        Dictionary<string, byte[]> GetUsersPhoto(string login);
        Dictionary<string, byte[]> GetAllPhoto();
        List<User> GetUsersList();
    }
}
