using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agendamento.Data;
using Agendamento.Models;
using Agendamento.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Agendamento.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> GetLogin([FromBody] User model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var Object = await context.Users.Where(u => u.UserName == model.UserName && u.Password == model.Password).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Usuario não encontrado!" });

                var Token = TokenService.GenerateToken(Object);

                Object.Password = "";

                return Ok(new
                {
                    user = Object,
                    token = Token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            try
            {
                var Users = await context.Users.AsNoTracking().ToListAsync();
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<User>> GetById(int id, [FromServices] DataContext context)
        {
            try
            {
                var User = await context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (User == null)
                    return NotFound(new { message = "não foi possivel encontrar nenhum usuario" });

                return Ok(User);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<User>> Post([FromBody] User model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<User>> Put([FromBody] User model, [FromServices] DataContext context)
        {
            try
            {
                var Object = await context.Users.Where(c => c.Id == model.Id).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Usuário não encontrado!" });

                context.Entry<User>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("id:int")]
        [Authorize]
        public async Task<ActionResult<User>> Delete(int id, [FromServices] DataContext context)
        {
            try
            {
                var Object = await context.Users.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Usere não encontrado!" });

                context.Users.Remove(Object);
                await context.SaveChangesAsync();
                return Ok(new { message = "Usere deletado com sucesso!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }


}