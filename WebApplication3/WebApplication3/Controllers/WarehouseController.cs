using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Context;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    /// <summary>
    /// 仓库
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {

        private readonly MyOracleContext _context;

        public WarehouseController(MyOracleContext context)
        {
            _context = context;
        }
        //private List<Warehouse> GetApps()
        //{
        //    List<Warehouse> list = new List<Warehouse>();
        //    list.Add(new Warehouse() { Name = "WeChat", Code = "WeChat" });
        //    list.Add(new Warehouse() { Name = "FaceBook", Code = "FaceBook" });
        //    list.Add(new Warehouse() { Name = "Google", Code = "Google" });
        //    list.Add(new Warehouse() { Name = "QQ", Code = "QQ" });
        //    return list;
        //}

        ///// <summary>
        ///// 获取Oracle数据库仓库信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public List<Warehouse> GetWarehouse()
        //{
        //    using (var db = new MyOracleContext())
        //    {
        //        //var count = db.warehouse.Where(x => x.Code == x.Code ).Count();
        //      //  List<Warehouse> todoItems = db.warehouse.Where(x => x.Code == x.Code).ToList();
        //        //_context.warehouse.
        //        // return await todoItems.Select(t => "a");
        //       return db.warehouse.Where(x => x.Code == x.Code).ToList();
        //      //  return await _context.warehouse.ToListAsync();

        //    }
           
        //}


        [HttpGet]

        public IEnumerable<string> GetAA()
        {
            List<Warehouse> todoItems = _context.warehouse.ToList();
            yield return todoItems.ToString();
        }

        ///// <summary>
        ///// 获取所有仓库信息
        ///// </summary>
        ///// <returns>所有仓库集合</returns>
        //[HttpGet]
        //public HttpResponseMessage Get()
        //{
        //    List<Warehouse> dclist = GetApps();
        //    return MyJson.ObjectToJson(dclist);
        //}

        ///// <summary>
        ///// 获取指定仓库
        ///// </summary>
        ///// <param name="code">需要获取仓库的code</param>
        ///// <returns>返回指定仓库</returns>
        //[HttpGet]
        //public HttpResponseMessage GetByCode(string code)
        //{
        //    var app = GetApps().Where(m => m.Code.Equals(code)).FirstOrDefault();
        //    return MyJson.ObjectToJson(app);
        //}

        ///// <summary>
        ///// 增加Warehouse信息
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public HttpResponseMessage Insert([FromBody]Warehouse value)
        //{
        //    ResultJson json = new ResultJson() { Code = 200, Message = "Ok" };
        //    return MyJson.ObjectToJson(json);
        //}

        ///// <summary>
        ///// 更新仓库信息
        ///// </summary>
        ///// <param name="value">仓库信息</param>
        ///// <returns>更新结果</returns>
        //[HttpPut]
        //public HttpResponseMessage UpdateApp([FromBody]Warehouse value)
        //{
        //    ResultJson json = new ResultJson() { Code = 200, Message = "Ok" };
        //    return MyJson.ObjectToJson(json);
        //}

        ///// <summary>
        ///// 删除仓库信息
        ///// </summary>
        ///// <param name="code">仓库编号</param>
        ///// <returns>删除结果</returns>
        //[HttpDelete]
        //public HttpResponseMessage DeleteApp(string code)
        //{
        //    ResultJson json = new ResultJson() { Code = 200, Message = "Ok" };
        //    return MyJson.ObjectToJson(json);
        //}

        /// <summary>
        /// 获取Oracle数据库所有仓库信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouse()
        {
            return await _context.warehouse.ToListAsync();
        }

        /// <summary>
        /// 获取Oracle数据库指定ID仓库信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(string id)
        {
            var warehouse = await _context.warehouse.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return warehouse;
        }

        /// <summary>
        /// 修改Oracle数据库指定仓库信息
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditWarehouse(string id, Warehouse warehouse)
        {
            if (id != warehouse.ID)
            {
                return BadRequest();
            }

            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
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
        /// 新增Oracle数据库仓库信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Warehouse>> AddWarehouse(Warehouse warehouse)
        {
            _context.warehouse.Add(warehouse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WarehouseExists(warehouse.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWarehouse", new { id = warehouse.ID }, warehouse);
        }

        /// <summary>
        /// 删除Oracle数据库指定仓库信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Warehouse>> DeleteWarehouse(string id)
        {
            var warehouse = await _context.warehouse.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.warehouse.Remove(warehouse);
            await _context.SaveChangesAsync();

            return warehouse;
        }

        private bool WarehouseExists(string id)
        {
            return _context.warehouse.Any(e => e.ID == id);
        }
    }
}