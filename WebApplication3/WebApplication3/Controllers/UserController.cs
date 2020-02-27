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
    /// <summary>
    /// 用户
    /// </summary>
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            /// <summary>
            /// 测试方法
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpGet]
            public ActionResult<string> Get(string id)
            {
                return id + " Say :Hello World!";
            }
            private readonly MyContext _context;
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="context"></param>
            public UserController(MyContext context)
            {
                _context = context;
                if (_context.Users.Count() == 0)
                {
                    _context.Users.Add(new Models.User { Name = "Admin" });
                    _context.SaveChanges();
                }
            }
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
            [HttpGet]
            public async Task<ActionResult<IEnumerable<User>>> GetUserItems()
            {
                return await _context.Users.ToListAsync();
            }
        /// <summary>
        /// 获取指定ID下的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
            [HttpGet("{id}")]
            public async Task<ActionResult<User>> GetUserItem(long id)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
        /// <summary>
        /// 获取指定ID下的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<User>> GetUserItem1(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
            public async Task<ActionResult<User>> AddUserItem(User user)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUserItem), new { id = user.Id, user });
            }
        /// <summary>
        /// 修改指定ID的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
            [HttpPut("{id}")]
            public async Task<IActionResult> EditTodoItem(long id, User user)
            {
                if (id != user.Id)
                {
                    return BadRequest();
                }
                _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        /// <summary>
        /// 删除指定ID的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteTodoItem(long id)
            {
                var todoItem = await _context.Users.FindAsync(id);
                if (todoItem == null)
                {
                    return NotFound();
                }
                _context.Users.Remove(todoItem);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }