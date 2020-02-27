using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    /// <summary>
    /// 仓库
    /// </summary>
    [Table("WAREHOUSE")]
    public class Warehouse
    {
        /// <summary>
        /// 仓库ID
        /// </summary>
        [Key]
        [Column("ID")]
        public string ID { set; get; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("NAME")]
        public string Name { set; get; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("CODE")]
        public string Code { set; get; }


    }
}
