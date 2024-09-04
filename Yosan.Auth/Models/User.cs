using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yosan.Auth.Models
{
    public class User
    {
        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;

        }

        private Guid Id { get; set; }
        private string Username { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
    }
}