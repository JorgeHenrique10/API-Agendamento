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
    [Route("schedules")]
    public class ScheduleController : ControllerBase
    {
        [HttpGet]
        [Route("date")]
        public async Task<ActionResult<List<Schedule>>> GetDate([FromBody] Schedule model, [FromServices] DataContext context)
        {
            var Schedules = await context.Schedules.AsNoTracking().Include(P => P.Procedure).Include(C => C.Client).Where(s => s.DateSchedule == model.DateSchedule).ToListAsync();
            return Ok(Schedules);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Schedule>>> Get([FromServices] DataContext context)
        {
            try
            {
                var Schedules = await context.Schedules.AsNoTracking().Include(P => P.Procedure).Include(C => C.Client).ToListAsync();
                return Ok(Schedules);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Schedule>> GetById(int id, [FromServices] DataContext context)
        {
            try
            {
                var Schedule = await context.Schedules.AsNoTracking().Include(P => P.Procedure).Include(C => C.Client).ToListAsync();

                if (Schedule == null)
                    return NotFound(new { message = "não foi possivel encontrar nenhum agendamento" });

                return Ok(Schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Schedule>> Post([FromBody] Schedule model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Schedules.Add(model);
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
        public async Task<ActionResult<Schedule>> Put([FromBody] Schedule model, [FromServices] DataContext context)
        {
            try
            {
                var Object = await context.Schedules.Where(c => c.Id == model.Id).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Agendamento não encontrado!" });

                context.Entry<Schedule>(model).State = EntityState.Modified;
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
        public async Task<ActionResult<Schedule>> Delete(int id, [FromServices] DataContext context)
        {
            try
            {
                var Object = await context.Schedules.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (Object == null)
                    return NotFound(new { message = "Agendamento não encontrado!" });

                context.Schedules.Remove(Object);
                await context.SaveChangesAsync();
                return Ok(new { message = "Agendamento deletado com sucesso!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }


}