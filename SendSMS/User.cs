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
            switch (numb)
            {
                case 1: Console.Write("Login: "); break;
                case 2: Console.Write("Password: "); break;
                case 3: Console.Write("Confirm Password: "); break;
                case 4: Console.Write("Email: "); break;
                case 5: Console.Write("Phone number: "); break;
            }
        }

        #region Ввод логина
        public void EnterLogin()
        {
            const int FIELD_NUMBER = 1;
            char[] prohibitedSymbols = { ' ', '/', '\\', ':', ';', '[', ']', '|', '=', ',', '+', '*', '?', '<', '>' };

            Show(FIELD_NUMBER);
            login = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("Логин содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                Console.ReadKey();
                Console.Clear();
                EnterLogin();
            }

            for (int i = 0; i < login.Length; i++)
            {
                for (int j = 0; j < prohibitedSymbols.Length; j++)
                {
                    if (login[i] == prohibitedSymbols[j])
                    {
                        Console.WriteLine("Логин содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                        Console.ReadKey();
                        Console.Clear();
                        EnterLogin();
                    }
                    else if((login[i] >= 'А' && login[i] <= 'Я') || (login[i] >= 'а' && login[i] <= 'я'))
                    {
                        Console.WriteLine("Логин содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                        Console.ReadKey();
                        Console.Clear();
                        EnterLogin();
                    }
                }
            }
        }
        #endregion
        #region Ввод пароля
        public void EnterPassword()
        {
            const int FIELD_NUMBER_2 = 2;
            char[] prohibitedSymbols = { ' ', '/', '\\', ':', ';', '[', ']', '|', '=', ',', '+', '*', '?', '<', '>' };

            Show(FIELD_NUMBER_2);
            password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Пароль содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                Console.ReadKey();
                Console.Clear();
                EnterPassword();
            }

            for (int i = 0; i < password.Length; i++)
            {
                for (int j = 0; j < prohibitedSymbols.Length; j++)
                {
                    if (password[i] == prohibitedSymbols[j])
                    {
                        Console.WriteLine("Пароль содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                        Console.ReadKey();
                        Console.Clear();
                        EnterPassword();
                    }
                    else if ((password[i] >= 'А' && password[i] <= 'Я') || (password[i] >= 'а' && password[i] <= 'я'))
                    {
                        Console.WriteLine("Пароль содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                        Console.ReadKey();
                        Console.Clear();
                        EnterPassword();
                    }
                }
            }
        }
        #endregion

        #region Подтверждение пароля
        public void ConfirmPassword()
        {
            const int FIELD_NUMBER_3 = 3;
            Show(FIELD_NUMBER_3);
            string confirmPassword = Console.ReadLine();

            if (password != confirmPassword)
            {
                Console.WriteLine("Пароли не совпадают, нажмите Enter чтобы ввести заново...");
                Console.ReadKey();
                Console.Clear();
                ConfirmPassword();
            }
        }
        #endregion

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
