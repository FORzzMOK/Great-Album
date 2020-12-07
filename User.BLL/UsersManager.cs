using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Common;

namespace Users.BLL
{
    public class UsersManager
    {
        private IUserStorable UsersStorage = Dependensies.UsersStorage;
        public User GetUserByLogin(string login)
        {
            return UsersStorage.GetUserByLogin(login);
        }
        public bool CheckLoginAndPass(string login, string password)
        {
            return UsersStorage.CheckLoginAndPass(login, password);
        }
        public bool CheckLogin(string login)
        {
            return UsersStorage.CheckLogin(login);
        }
        public bool AddUser(User user)
        {
            return UsersStorage.AddUser(user);
        }
        public bool EditName(string login, string newName)
        {
            return UsersStorage.EditName(login, newName);
        }
        public bool EditSurname(string login, string newSurname)
        {
            return UsersStorage.EditSurname(login, newSurname);
        }
        public bool EditStatus(string login, string status)
        {
            return UsersStorage.EditStatus(login, status);
        }
        public bool EditDateOfBirth(string login, DateTime newDateOfBirth)
        {
            return UsersStorage.EditDateOfBirth(login, newDateOfBirth);
        }
        public bool EditEmail(string login, string newEmail)
        {
            return UsersStorage.EditEmail(login, newEmail);
        }
        public bool EditAvatar(string login, byte[] newAvatar)
        {
            return UsersStorage.EditAvatar(login, newAvatar);
        }
        public bool AddPhoto(string login, byte[] newPhoto, string title)
        {
            return UsersStorage.AddPhoto(login, newPhoto, title);
        }
        public Dictionary<string, byte[]> GetUsersPhoto(string login)
        {
            return UsersStorage.GetUsersPhoto(login);
        }
        public Dictionary<string, byte[]> GetAllPhoto()
        {
            return UsersStorage.GetAllPhoto();
        }
        public List<User> GetUsersList()
        {
            return UsersStorage.GetUsersList();
        }
    }
}
