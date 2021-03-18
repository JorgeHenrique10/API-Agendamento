using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agendamento.Data;
using Agendamento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Controllers
{
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Client>>> Get([FromServices] DataContext context)
        {
            try
            {
                var clients = await context.Clients.AsNoTracking().ToListAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Client>> GetById(int id, [FromServices] DataContext context)
        {
            try
            {
                var Client = await context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (Client == null)
                    return NotFound(new { message = "não foi possivel encontrar nenhum usuario" });

                return Ok(Client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Client>> Post([FromBody] Client model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Clients.Add(model);
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
        public async Task<ActionResult<Client>> Put([FromBody] Client model, [FromServices] DataContext context)
        {
            try
            {
                var Object = await context.Clients.Where(c => c.Id == model.Id).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Cliente não encontrado!" });

                context.Entry<Client>(model).State = EntityState.Modified;
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
        public async Task<ActionResult<Client>> Delete(int id, [FromServices] DataContext context)
        {
            try
            {
                var Object = await context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Cliente não encontrado!" });

                context.Clients.Remove(Object);
                await context.SaveChangesAsync();
                return Ok(new { message = "Cliente deletado com sucesso!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }


}