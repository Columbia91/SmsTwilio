using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SendSMS
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();

            user.EnterLogin();
            user.EnterPassword();
            user.ConfirmPassword();
            user.EnterEmail();
            user.EnterPhoneNumber();

            string adoptedCode = VerificationAccount();

            Console.WriteLine("We sent your phone number verification code, please enter");
            string verificationCode = Console.ReadLine();

            if (verificationCode == adoptedCode)
            {
                Console.Clear();
                Console.WriteLine("Congratulations! You succesfully passed registration");
            }

            Console.ReadLine();
        }
        static string VerificationAccount()
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
                to: new Twilio.Types.PhoneNumber("+77719777518")
            );
            
            Console.WriteLine(message.Sid);

            return code;
        }
    }
}
