using System;
using System.Configuration;

namespace YouVote.Model.Plugin.Security
{
    public class PasswordGenerator
    {
        public Random Generator { get; set; }
        public int PasswordLength { get; set; }
        public char[] PosibleChar { get; set; }

        public PasswordGenerator()
        {
            Generator = new Random();
            PasswordLength = ConfigurationManager.AppSettings["passwordLength"] != null 
                ? Convert.ToInt32(ConfigurationManager.AppSettings["passwordLength"]) 
                : 8;
            PosibleChar = ConfigurationManager.AppSettings["passwordChars"] != null
                ? ConfigurationManager.AppSettings["passwordChars"].ToCharArray()
                : "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?".ToCharArray();
        }

        protected int GetRandomNumber()
        {
            return Generator.Next(0, PosibleChar.Length);
        }

        public string Run()
        {
            var res = string.Empty;

            while (res.Length < PasswordLength)
            {
                res += PosibleChar[GetRandomNumber()];
            }

            return res;
        }

        public static string Generate()
        {
            var generator = new PasswordGenerator();
            return generator.Run();
        }

        public static string Generate(int passwordLength)
        {
            var generator = new PasswordGenerator {PasswordLength = passwordLength};
            return generator.Run();
        }
    }
}