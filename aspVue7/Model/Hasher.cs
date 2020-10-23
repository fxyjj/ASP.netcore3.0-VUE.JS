using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace aspVue7.Model
{
    public class PasswordHasher
    {
        private static string HashPassword(string value,string salt)
        {
            var cipherBytes = KeyDerivation.Pbkdf2(
                password:value,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount:10000,
                numBytesRequested: 256/8
            );
            return Convert.ToBase64String(cipherBytes);
        }

        public static string HasPassword(string password)
        {
            var salt = GenerateSalt();
            var hash = HashPassword(password,salt);
            var res = $"{salt}.{hash}";
            return res;
        }

        private static bool Validate(string password, string salt,string hash) => HashPassword(password,salt) == hash;

        public static bool VerifyHashedPassword(string password,string storePassword)
        {
            
            if(string.IsNullOrEmpty(password))
            {
                //密码为空
                Console.WriteLine("密码为空");
            }

            if(string.IsNullOrEmpty(storePassword))
            {
                //数据库中密码为空
                Console.WriteLine("数据库中密码为空");
            }

            var parts = storePassword.Split('.');
            var salt = parts[0];
            var hash = parts[1];

            return Validate(password,salt,hash);
        }

        private static string GenerateSalt()
        {
            byte[] randomBytes = new byte[128/8];
            using(var generator = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
