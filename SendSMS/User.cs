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
        private string email;
        private string phoneNumber;

        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public void Show(int numb)
        {
            for (int i = numb; i <= numb; i++)
            {
                switch (i)
                {
                    case 1: Console.Write("Login: " + login); break;
                    case 2: Console.Write("Password: " + password); break;
                    case 3: Console.Write("Confirm Password: " + password); break;
                    case 4: Console.Write("Email: " + email); break;
                    case 5: Console.Write("Phone number: " + PhoneNumber); break;
                }
            }
        }
        public void EnterLogin()
        {
            const int FIELD_NUMBER = 1;
            char[] prohibitedSymbols = { '/', '\\', ':', ';', '[', ']', '|', '=', ',', '+', '*', '?', '<', '>' };

            Show(FIELD_NUMBER);
            login = Console.ReadLine();

            //if (string.IsNullOrWhiteSpace(login))
            //{
            //    Console.WriteLine("Логин содержит недопустимые символы");
            //}

            for (int i = 0; i < login.Length; i++)
            {
                for (int j = 0; j < prohibitedSymbols.Length; j++)
                {
                    if (login[i] == prohibitedSymbols[j])
                    {
                        Console.WriteLine("Логин содержит недопустимые символы");
                    }

                }
            }
        }
        public void EnterPassword()
        {
            const int FIELD_NUMBER_2 = 2, FIELD_NUMBER_3 = 3;
            char[] prohibitedSymbols = { '/', '\\', ':', ';', '[', ']', '|', '=', ',', '+', '*', '?', '<', '>' };

            Show(FIELD_NUMBER_2);
            password = Console.ReadLine();
            for (int i = 0; i < password.Length; i++)
            {
                for (int j = 0; j < prohibitedSymbols.Length; j++)
                {
                    if (password[i] == prohibitedSymbols[j])
                    {
                        Console.WriteLine("Пароль содержит недопустимые символы");
                    }

                }
            }

            Show(FIELD_NUMBER_3);
            string confirmPassword = Console.ReadLine();

            if(password != confirmPassword)
            {
                Console.WriteLine("Пароли не совпадают");
            }
        }
        public void EnterEmail()
        {
            const int FIELD_NUMBER_4 = 4;
            Show(FIELD_NUMBER_4);
            email = Console.ReadLine();
        }
        public void EnterPhoneNumber()
        {
            const int FIELD_NUMBER_5 = 5;
            Show(FIELD_NUMBER_5);
            phoneNumber = (Console.ReadLine());
        }
    }
}
