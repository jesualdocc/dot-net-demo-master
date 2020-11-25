using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APP_Demo__WebAPI_.Models;
using Demo_API;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugInfoController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DrugInfoController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/DrugInfo
        [HttpGet]
        public IEnumerable<DrugInfo> GetDrugInfos()
        {
            return _context.DrugInfos;
        }

        // GET: api/DrugInfo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrugInfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drugInfo = await _context.DrugInfos.FindAsync(id);

            if (drugInfo == null)
            {
                return NotFound();
            }

            return Ok(drugInfo);
        }

        // PUT: api/DrugInfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrugInfo([FromRoute] int id, [FromBody] DrugInfo drugInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drugInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(drugInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrugInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DrugInfo
        [HttpPost]
        public async Task<IActionResult> PostDrugInfo([FromBody] DrugInfo drugInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DrugInfos.Add(drugInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrugInfo", new { id = drugInfo.Id }, drugInfo);
        }

        // DELETE: api/DrugInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrugInfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drugInfo = await _context.DrugInfos.FindAsync(id);
            if (drugInfo == null)
            {
                return NotFound();
            }

            _context.DrugInfos.Remove(drugInfo);
            await _context.SaveChangesAsync();

            return Ok(drugInfo);
        }

        private bool DrugInfoExists(int id)
        {
            return _context.DrugInfos.Any(e => e.Id == id);
        }
    }
}