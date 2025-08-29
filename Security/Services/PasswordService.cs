using Core.Interfaces.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Security.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password, out string salt)
        {
            salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch
            {
                return false;
            }
        }

        public string GenerateSecurePassword(int length = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            var random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;

            // Al menos una minúscula, una mayúscula, un número y un carácter especial
            var hasLower = Regex.IsMatch(password, @"[a-z]");
            var hasUpper = Regex.IsMatch(password, @"[A-Z]");
            var hasDigit = Regex.IsMatch(password, @"\d");
            var hasSpecial = Regex.IsMatch(password, @"[!@#$%^&*(),.?\"":{}|<>]");

            return hasLower && hasUpper && hasDigit && hasSpecial;
        }
    }
}
