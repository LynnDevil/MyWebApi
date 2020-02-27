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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TUserController : ControllerBase
    {
        private readonly MySqlServerContext _context;

        public TUserController(MySqlServerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取SQLServer数据库所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tuser>>> GetTuser()
        {
            return await _context.Tuser.ToListAsync();
        }

        /// <summary>
        /// 获取SQLServer数据库指定ID用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Tuser>> GetTuser(int id)
        {
            var tuser = await _context.Tuser.FindAsync(id);

            if (tuser == null)
            {
                return NotFound();
            }

            return tuser;
        }

        /// <summary>
        /// 修改SQLServer数据库指定用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTuser(int id, Tuser tuser)
        {
            if (id != tuser.Id)
            {
                return BadRequest();
            }

            _context.Entry(tuser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TuserExists(id))
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
        /// 新增SQLServer数据库用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Tuser>> AddTuser(Tuser tuser)
        {
            _context.Tuser.Add(tuser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TuserExists(tuser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTuser", new { id = tuser.Id }, tuser);
        }

        /// <summary>
        /// 删除SQLServer数据库指定用户信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tuser>> DeleteTuser(int id)
        {
            var tuser = await _context.Tuser.FindAsync(id);
            if (tuser == null)
            {
                return NotFound();
            }

            _context.Tuser.Remove(tuser);
            await _context.SaveChangesAsync();

            return tuser;
        }

        private bool TuserExists(int id)
        {
            return _context.Tuser.Any(e => e.Id == id);
        }
    }
}
