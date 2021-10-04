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
        public const int validadeToken = 5;
        public static string Token { get => GetToken(); }

        private static string GetToken()
        {
            return GenericService.GenerateToken(6);
        }

        public static void HidePassword(User user)
        {
            user.Senha = mask;
        }
    }
}
