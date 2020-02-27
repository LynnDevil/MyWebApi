using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Context
{
    public partial class MyOracleContext : DbContext
    {
        public MyOracleContext()
        {
        }

        public MyOracleContext(DbContextOptions<MyOracleContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseOracle(@"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.8.45)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=zncc_isa;Password=ntidba", b => b.UseOracleSQLCompatibility("11"));
        //    }
        //}

        public DbSet<Warehouse> warehouse { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Warehouse>(entity =>
        //    {
        //        entity.HasKey(e => e.ID)
        //            .HasName("PRIMARY");

        //        entity.ToTable("WAREHOUSE");

        //        entity.Property(e => e.ID)
        //            .HasColumnName("ID")
        //            .HasColumnType("varchar(50)");

        //        entity.Property(e => e.Name)
        //            .HasColumnName("NAME")
        //            .HasColumnType("varchar(50)");

        //        entity.Property(e => e.Code)
        //            .HasColumnName("CODE")
        //            .HasColumnType("varchar(50)");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}