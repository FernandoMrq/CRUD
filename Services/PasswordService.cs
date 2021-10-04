using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Services
{
    public class PasswordService
    {
        private const string mask = "******";
        public static string Token { get => GetToken(); }

        private static string GetToken()
        {
            return GenerateToken(6);
        }

        public static void HidePassword(User user)
        {
            user.Senha = mask;
        }

        private  static string GenerateToken(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
