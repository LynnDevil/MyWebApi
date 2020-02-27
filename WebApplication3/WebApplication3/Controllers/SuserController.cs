using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Context;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuserController : ControllerBase
    {
        private readonly MySqlContext _context;

        public SuserController(MySqlContext context)
        {
            _context = context;
        }

       /// <summary>
       /// 获取MySql数据库用户信息
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suser>>> GetSuser()
        {
            return await _context.Suser.ToListAsync();
        }

        /// <summary>
        /// 获取MySql数据库指定用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Suser>> GetSuser(int id)
        {
            var suser = await _context.Suser.FindAsync(id);

            if (suser == null)
            {
                return NotFound();
            }

            return suser;
        }

        /// <summary>
        /// 修改MySql数据库指定用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSuser(int id, Suser suser)
        {
            if (id != suser.Iduser)
            {
                return BadRequest();
            }

            _context.Entry(suser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuserExists(id))
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

        /// <summary>
        /// 新增MySql数据库用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Suser>>AddSuser(Suser suser)
        {
            _context.Suser.Add(suser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuser", new { id = suser.Iduser }, suser);
        }

        /// <summary>
        /// 删除MySql数据库用户信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Suser>> DeleteSuser(int id)
        {
            var suser = await _context.Suser.FindAsync(id);
            if (suser == null)
            {
                return NotFound();
            }

            _context.Suser.Remove(suser);
            await _context.SaveChangesAsync();

            return suser;
        }

        private bool SuserExists(int id)
        {
            return _context.Suser.Any(e => e.Iduser == id);
        }
    }
}
