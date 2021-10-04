using CRUD.Models;
using CRUD.Repositories;
using CRUD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    [Authorize]
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
            User userBanco = await repository.GetById(user.Id);

            if ((user == null) || (user.Id == 0) || (userBanco == null))
            {
                return BadRequest("Usuário inválido");
            }

            try
            {
                ResetPasswordToken token = repository.GetLastToken(userBanco);

                if (!(TokenService.IsValid(token)) || (token.Token != GenericService.hashString(user.Token))){
                    return BadRequest("Token expirado ou inválido");
                }
                else if (userBanco.Senha != GenericService.hashString(user.Senha))
                {
                    return BadRequest("Senha original inválida");
                }
                else if (user.SenhaNova.Trim() == "")
                {
                    return BadRequest("Senha inválida");
                }

                userBanco.Senha = GenericService.hashString(user.SenhaNova);

                await repository.UpdateUserPassword(userBanco);

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok("Senha alterada com sucesso");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            string token = PasswordService.Token;

            var user = await repository.GetById(id);

            if (user == null)
            {
                return NotFound("Usuario não encontrado pelo id informado");
            }

            ResetPasswordToken reset = new ResetPasswordToken() {

                Cadastro = DateTime.Now,
                Token = GenericService.hashString(token),
                Validade = PasswordService.validadeToken
            };

            user.ResetPasswordToken.Add(reset);

            await repository.Update(user);

            return Ok($"Seu token para reset de senha é {token}");
        }
    }
}
