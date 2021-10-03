using CRUD.Models;
using CRUD.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository repository;
        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await repository.GetAll();
            if(users == null)
            {
                return BadRequest();
            }
            return Ok(users.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await repository.GetById(id);

            if (user == null)
            {
                return NotFound("Usuario não encontrado pelo id informado");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Usuário inválido");
            }

            await repository.Insert(user);

            return CreatedAtAction(nameof(GetUser), new { Id = user.Id }, user);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Usuário inválido");
            }

            try
            {
                await repository.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok("Atualização do usuário realizada com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await repository.GetById(id);
            if (user == null)
            {
                return NotFound($"Usuário de {id} não encontrado");
            }

            await repository.Delete(id);

            return Ok(user);
        }
    }
}
