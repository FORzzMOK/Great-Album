using System;
using Entities;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Users.DAL
{
    public class UserMemoryStorage : IUserStorable
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public User GetUserByLogin(string login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE Login='{login}'", connection);
                SqlDataReader reader = command.ExecuteReader();
                User user = new User();
                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        DateTime.TryParse($"{reader.GetValue(5)}", out DateTime myDate);
                        user = new User()
                        {
                            Id = int.Parse($"{reader.GetValue(0)}"),
                            Login = $"{reader.GetValue(1)}",
                            Name = $"{reader.GetValue(2)}",
                            Surname = $"{reader.GetValue(3)}",
                            Status = $"{reader.GetValue(4)}",
                            DateOfBirth = myDate,
                            Email = $"{reader.GetValue(6)}",
                            Avatar = (byte[])reader.GetValue(8)
                        };
                    }
                }
                reader.Close();
                return user;
            }
        }
        public bool CheckLoginAndPass(string login, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT Password FROM Users WHERE Login='{login}'", connection);
                SqlDataReader reader = command.ExecuteReader();
                string myPass = null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        {
                            myPass = $"{reader.GetValue(0)}";
                        };
                    }
                }
                reader.Close();
                return myPass == password;
            }
        }
        public bool CheckLogin(string login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT ID FROM Users WHERE Login='{login}'", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
                return false;
            }
        }
        public bool AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Image image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"Pages\Images\avatar\stub.png");
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] image_byte = memoryStream.ToArray();

                connection.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO Users (Login, Name, Surname, Password, Avatar) VALUES ('{user.Login}', '{user.Name}', '{user.Surname}', '{user.Password}', @Avatar)", connection);
                command.Parameters.Add("@Avatar", SqlDbType.VarBinary, 8000).Value = image_byte;
                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool EditName(string login, string newName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE Users SET Name='{newName}' WHERE Login='{login}'", connection);
                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool EditSurname(string login, string newSurname)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE Users SET Surname='{newSurname}' WHERE Login='{login}'", connection);
                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool EditStatus(string login, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE Users SET Status='{status}' WHERE Login='{login}'", connection);
                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool EditDateOfBirth(string login, DateTime newDateOfBirth)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE Users SET DateOfBirth='{newDateOfBirth}' WHERE Login='{login}'", connection);
                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool EditEmail(string login, string newEmail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE Users SET Email='{newEmail}' WHERE Login='{login}'", connection);
                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool EditAvatar(string login, byte[] newAvatar)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE Users SET Avatar=@Avatar WHERE Login='{login}'", connection);
                command.Parameters.Add("@Avatar", SqlDbType.VarBinary).Value = newAvatar;
                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool AddPhoto(string login, byte[] newPhoto, string title)
        {
            int PhotoID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO Photo (Image, Title) VALUES (@Photo, '{title}');SET @ID=SCOPE_IDENTITY()", connection);
                command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = newPhoto;
                SqlParameter idParam = new SqlParameter("@ID", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
                PhotoID = int.Parse($"{idParam.Value}");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO Communication (UserLogin, PhotoID) VALUES ('{login}', '{PhotoID}')", connection);
                command.ExecuteNonQuery();
            }
            return true;
        }
        public Dictionary<string, byte[]> GetUsersPhoto(string login)
        {
            Dictionary<string, byte[]> photoList = new Dictionary<string, byte[]>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT Image, Title FROM Photo, Communication  WHERE Communication.UserLogin = '{login}' AND Communication.PhotoID = Photo.ID", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        {
                            photoList.Add($"{reader.GetValue(1)}", (byte[])reader.GetValue(0));
                        };
                    }
                }
                reader.Close();
                return photoList;
            }
        }
        public Dictionary<string, byte[]> GetAllPhoto()
        {
            Dictionary<string, byte[]> photoList = new Dictionary<string, byte[]>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT Image, Title  FROM Photo", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        {
                            photoList.Add($"{reader.GetValue(1)}", (byte[])reader.GetValue(0));
                        };
                    }
                }
                reader.Close();
                return photoList;
            }
        }
        public List<User> GetUsersList()
        {
            List<User> UsersList = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Users", connection);
                SqlDataReader reader = command.ExecuteReader();
                User user = new User();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        DateTime.TryParse($"{reader.GetValue(5)}", out DateTime myDate);
                        user = new User()
                        {
                            Id = int.Parse($"{reader.GetValue(0)}"),
                            Login = $"{reader.GetValue(1)}",
                            Name = $"{reader.GetValue(2)}",
                            Surname = $"{reader.GetValue(3)}",
                            Status = $"{reader.GetValue(4)}",
                            DateOfBirth = myDate,
                            Email = $"{reader.GetValue(6)}",
                            Avatar = (byte[])reader.GetValue(8)
                        };
                        UsersList.Add(user);
                    }
                }
                reader.Close();
                return UsersList;
            }
        }
    }
}
