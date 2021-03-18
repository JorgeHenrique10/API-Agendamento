using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agendamento.Data;
using Agendamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Controllers
{
    [Route("procedures")]
    public class ProcedureController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Procedure>>> Get([FromServices] DataContext context)
        {
            try
            {
                var Procedures = await context.Procedures.AsNoTracking().ToListAsync();
                return Ok(Procedures);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Procedure>> GetById(int id, [FromServices] DataContext context)
        {
            try
            {
                var Procedure = await context.Procedures.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (Procedure == null)
                    return NotFound(new { message = "não foi possivel encontrar nenhum usuario" });

                return Ok(Procedure);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Procedure>> Post([FromBody] Procedure model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Procedures.Add(model);
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
        public async Task<ActionResult<Procedure>> Put([FromBody] Procedure model, [FromServices] DataContext context)
        {
            try
            {
                var Object = await context.Procedures.Where(c => c.Id == model.Id).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Procedimento não encontrado!" });

                context.Entry<Procedure>(model).State = EntityState.Modified;
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
        public async Task<ActionResult<Procedure>> Delete(int id, [FromServices] DataContext context)
        {
            try
            {
                var Object = await context.Procedures.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Procedimento não encontrado!" });

                context.Procedures.Remove(Object);
                await context.SaveChangesAsync();
                return Ok(new { message = "Procedimento deletado com sucesso!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }


}