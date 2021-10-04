using CRUD.Models;
using CRUD.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : Controller
    {
        private readonly IUserRepository repository;
        public ChangePasswordController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(UserResetPassword user)
        {
            if ((user == null) || (user.Id == 0))
            {
                return BadRequest("Usuário inválido");
            }

            try
            {
                await repository.UpdateUserName(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok("Senha alterada com sucesso");
        }
    }
}
