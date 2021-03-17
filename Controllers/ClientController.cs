using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agendamento.Data;
using Agendamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Controllers
{
    [Route("")]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        [Route("")]
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
    }
}