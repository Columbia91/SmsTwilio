using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendSMS
{
    class User
    {
        private string login;
        private string password;
        private string passwordCopy;
        private string email;
        private string phoneNumber;

        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public string PasswordCopy { get => passwordCopy; set => passwordCopy = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        
    }
}