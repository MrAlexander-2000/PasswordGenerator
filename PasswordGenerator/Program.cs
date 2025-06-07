using System;
using System.Text;
using System.Security.Cryptography;

namespace PasswordGenerator
{
    class Program
    {
        static int passwordLengthParsed;
        static int passwordComplexityParsed;
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Greet();
            GatherInfo();
        }

        private static void Greet()
        {
            int timeCurrentHour = DateTime.Now.Hour;
            string greeting = "\nWelcome to this password generator!";

            if (timeCurrentHour < 12)
            {
                Console.WriteLine("Good morning!" + greeting);
            }

            else if (timeCurrentHour >= 12)
            {
                Console.WriteLine("Good afternoon!" + greeting);
            }

            else if (timeCurrentHour >= 19)
            {
                Console.WriteLine("Good evening!" + greeting);
            }
        }

        private static void GatherInfo()
        {
            Console.WriteLine("Please enter the 'desired' password length (Min: 8, Max: 128): ");
            var passwordLength = Console.ReadLine();

            try
            {
                passwordLengthParsed = int.Parse(passwordLength);
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("You have entered text instead of a number. Please enter a valid number.");
                GatherInfo();
                return;
            }


            if (passwordLengthParsed < 8 || passwordLengthParsed > 128)
            {
                Console.Clear();
                Console.WriteLine("Invalid password length. Please enter a value between 8 and 128.");
                GatherInfo();
            }

            else
            {
                Console.Clear();
                Console.WriteLine($"You have chosen a password length of {passwordLength} characters.\n");
                GatherInfo_2();
            }
           }

        private static void GatherInfo_2()
        {
            Console.WriteLine("Please enter the desired password complexity (1-3):\n" +
                             "1: Only Lowercase Letters & Digits\n" +
                             "2: Only Uppercase/Lowercase Letters & Digits\n" +
                             "3: Uppercase/Lowercase Letters & Digits & Special Characters");

            var passwordComplexityOption = Console.ReadLine();

            try
            {
                passwordComplexityParsed = int.Parse(passwordComplexityOption);
            }

            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("You have entered text instead of a number. Please enter a valid complexity option number.");
                GatherInfo_2();
                return;
            }

            if (passwordComplexityParsed < 1 || passwordComplexityParsed > 3)
            {
                Console.Clear();
                Console.WriteLine("Invalid option. Please enter a valid complexity option number between 1 and 3.");
                GatherInfo_2();
            }

            else
            {
                Console.Clear();
                Console.WriteLine($"You have chosen option {passwordComplexityParsed} for password complexity.\n");
                Console.WriteLine("Generating your password...\n");

                if (passwordComplexityParsed == 1)
                {
                    GeneratePassword(passwordLengthParsed, passwordComplexityParsed);
                }

                else if (passwordComplexityParsed == 2)
                {
                    GeneratePassword(passwordLengthParsed, passwordComplexityParsed);
                }

                else if (passwordComplexityParsed == 3)
                {
                    GeneratePassword(passwordLengthParsed, passwordComplexityParsed);
                }
            }
        }

        private static void GeneratePassword(int passwordLength, int passwordComplexity)
        {
            string GeneratedPass = CreatePassword(passwordLength);
            Console.WriteLine($"Your generated password is: {GeneratedPass}\n");
            Console.WriteLine($"Sucessfully generated password of length {passwordLength} with complexity option {passwordComplexity}.");
        }

        public static string CreatePassword(int length)
        {
            length = passwordLengthParsed;
            const string valid_option_1 = "abcdefghijklmnopqrstuvwxyz1234567890";
            const string valid_option_2 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string valid_option_3 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+[]{}|;:,.<>?";

            string validChars = passwordComplexityParsed switch
            {
                1 => valid_option_1,
                2 => valid_option_2,
                3 => valid_option_3,
                _ => valid_option_1
            };

            StringBuilder res = new StringBuilder();
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] uintBuffer = new byte[4];
                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }
            return res.ToString();
        }
    }
}