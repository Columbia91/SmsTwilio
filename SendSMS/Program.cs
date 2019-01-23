using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Twilio; // консоль диспетчера пакетов Install-Package Twilio
using Twilio.Rest.Api.V2010.Account;

namespace SendSMS
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();

            EnterLogin(user);
            EnterPassword(user);
            ConfirmPassword(user);
            EnterEmail(user);
            EnterPhoneNumber(user);

            string adoptedCode = VerificationAccount(user);

            Console.WriteLine("\nМы отправили на указанный Вами номер код верификации, введите его ниже...");

            while (true)
            {
                Console.Write("\nКод подтверждения: ");
                string verificationCode = Console.ReadLine();
                if (verificationCode == adoptedCode)
                {
                    Console.Clear();
                    Console.WriteLine("Поздравляем! Вы успешно прошли регистрацию");
                }
                else
                {
                    Console.WriteLine("Неверный код верификации, нажмите Enter чтобы ввести заново...");
                    Console.ReadKey();
                    Console.Clear();
                    Show(user, 5, GiveMeStars(user), GiveMeStars(user));
                }
            }

            Console.ReadLine();
        }

        #region Вывод на консоль
        static void Show(User user, int numb, string stars = "", string stars2 = "")
        {
            for (int i = 0; i < numb; i++)
            {
                switch (i + 1)
                {
                    case 1: Console.Write("Login: " + user.Login); break;
                    case 2: Console.Write("\nPassword: " + stars); break;
                    case 3: Console.Write("\nConfirm Password: " + stars2); break;
                    case 4: Console.Write("\nEmail: " + user.Email); break;
                    case 5: Console.Write("\nPhone number (+XYYYZZZZZZZ): " + user.PhoneNumber); break;
                }
            }
        }
        #endregion

        #region Ввод логина
        static void EnterLogin(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER = 1, MIN_LOGIN_LENGTH = 3;

            Show(user, FIELD_NUMBER);
            user.Login = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(user.Login))
            {
                Console.WriteLine("Логин содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                user.Login = "";
                Console.ReadKey();
                EnterLogin(user);
            }

            Regex regex = new Regex(@"\W|[А-Яа-я]+");
            MatchCollection matches = regex.Matches(user.Login);
            if (matches.Count > 0)
            {
                Console.WriteLine("Логин содержит недопустимые символы, нажмите Enter чтобы ввести заново...");
                user.Login = "";
                Console.ReadKey();
                EnterLogin(user);
            }
            if (user.Login.Length < MIN_LOGIN_LENGTH)
            {
                Console.WriteLine("Логин слишком короткий, нажмите Enter чтобы ввести заново...");
                user.Login = "";
                Console.ReadKey();
                EnterLogin(user);
            }
        }
        #endregion

        #region Ввод пароля
        static void EnterPassword(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER_2 = 2;

            Show(user, FIELD_NUMBER_2);
            user.Password = HideCharacter();
            user.Password = user.Password.TrimEnd(user.Password[user.Password.Length - 1]);

            string pattern = @"(?=^.{6,32}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

            while (true)
            {
                if (user.Password.Length < 6 || user.Password.Length > 32)
                    Console.WriteLine("Длина пароля должна быть не меньше 6 символов и не больше 32 символов, нажмите Enter чтобы ввести заново...");
                else if (!(Regex.IsMatch(user.Password, pattern)))
                    Console.WriteLine("Пароль должен содержать цифровой символ, и буквы верхнего, нижнего регистра, нажмите Enter чтобы ввести заново...");
                else
                    break;
                user.Password = "";
                Console.ReadKey();
                EnterPassword(user);
            }
        }
        #endregion

        #region Подтверждение пароля
        static void ConfirmPassword(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER_3 = 3;
            string stars = GiveMeStars(user);

            Show(user, FIELD_NUMBER_3, stars);
            user.PasswordCopy = HideCharacter();
            user.PasswordCopy = user.PasswordCopy.TrimEnd(user.PasswordCopy[user.PasswordCopy.Length - 1]);

            if (user.Password != user.PasswordCopy)
            {
                Console.WriteLine("Пароли не совпадают, нажмите Enter чтобы ввести заново...");
                user.PasswordCopy = "";
                Console.ReadKey();
                ConfirmPassword(user);
            }
        }
        #endregion

        #region Ввод почты
        static void EnterEmail(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER_4 = 4;
            string stars = GiveMeStars(user);

            Show(user, FIELD_NUMBER_4, stars, stars);
            user.Email = Console.ReadLine();

            string pattern = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";

            if (!(Regex.IsMatch(user.Email, pattern)))
            {
                Console.WriteLine("Некорректный email, нажмите Enter чтобы ввести заново...");
                user.Email = "";
                Console.ReadKey();
                EnterEmail(user);
            }
        }
        #endregion

        #region Ввод номера
        static void EnterPhoneNumber(User user)
        {
            Console.Clear();
            const int FIELD_NUMBER_5 = 5;
            string stars = GiveMeStars(user);

            Show(user, FIELD_NUMBER_5, stars, stars);
            user.PhoneNumber= Console.ReadLine();

            string pattern = @"^\+?[7]\d{10}$";

            if (!(Regex.IsMatch(user.PhoneNumber, pattern, RegexOptions.IgnoreCase)))
            {
                Console.WriteLine("Телефонный номер содержит недопустимые символы или введен не корректно, нажмите Enter чтобы ввести заново...");
                user.PhoneNumber = "";
                Console.ReadKey();
                EnterPhoneNumber(user);
            }
        }
        #endregion

        #region Верификация аккаунта
        static string VerificationAccount(User user)
        {
            // Find your Account Sid and Token at twilio.com/console
            const string accountSid = "AC9dc0d107f661a29db6e2db341af2beb4";
            const string authToken = "d7d8170696c1de5bda1e96b4a9347018";

            Random rnd = new Random();
            string code = Convert.ToString(rnd.Next(100, 1000));

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("+18432585652"),
                body: code,
                to: new Twilio.Types.PhoneNumber(user.PhoneNumber) // user.PhoneNumber
            );
            
            // Console.WriteLine(message.Sid);
            return code;
        }
        #endregion

        #region Скрытие пароля
        public static string HideCharacter()
        {
            ConsoleKeyInfo key;
            string code = "";
            do
            {
                key = Console.ReadKey(true);
                Console.Write("*");
                code += key.KeyChar;
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine("\n" + code);
            return code;
        }
        #endregion

        #region Звездочки
        public static string GiveMeStars(User user)
        {
            char[] symbols = new char[user.Password.Length];

            for (int i = 0; i < user.Password.Length; i++)
            {
                symbols[i] = '*';
            }
            string str = new string(symbols);
            return str;
        }
        #endregion
    }
}
