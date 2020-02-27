using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Context
{
    public class MyContext : DbContext
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        /// <param name="options"></param>
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        /// <summary>
        /// 用户信息类
        /// </summary>
        public DbSet<User> Users { get; set; }
    }

}
