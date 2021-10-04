using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Services
{
    public class TokenService
    {
        public static bool IsValid(ResetPasswordToken token)
        {
            return IsDateValid(token);
        }
        private static bool IsDateValid(ResetPasswordToken token)
        {
            return GenericService.DaysBetween(token.Cadastro, DateTime.Now) <= token.Validade;
        }
    }
}
