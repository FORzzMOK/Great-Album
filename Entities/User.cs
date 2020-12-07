using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        public List<string> Friends = new List<string>();
        public List<string> Photos = new List<string>();
        public override string ToString()
        {
            return $"{Name}\t{Surname}";
        }
    }
}
