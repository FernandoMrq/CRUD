using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Services
{
    public class GenericService
    {
        public static string GenerateToken(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static string hashGenerate(byte[] senha)
        {
            SHA512 hasher = SHA512.Create();
            byte[] hashBytes = hasher.ComputeHash(senha);
            string hash = Convert.ToBase64String(hashBytes);
            hasher.Clear();
            return hash;
        }
        public static string hashString(string senha)
        {
            byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
            return hashGenerate(bytesSenha);
        }

        public static int DaysBetween(DateTime dt1, DateTime dt2)
        {
            TimeSpan span = dt2.Subtract(dt1);
            return (int)span.TotalDays;
        }
    }
}
