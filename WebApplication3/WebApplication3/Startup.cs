using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication3.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.IO;

namespace WebApplication3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var title = "myApi";
            var version = "v1";
            var xmlFile = @"C:\Users\Administrator\source\repos\WebApplication3\WebApplication3\WebApplication3.xml";
            services.AddControllers();

            //EF Core 内存中数据库
            services.AddDbContext<MyContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            
            //SQL Server 2012 以上版本
            var connection = @"Server=127.0.0.1;User Id=sa;Password=ntidba;Database=MySqlServer;Persist Security Info=True";
            services.AddDbContext<MySqlServerContext>(options => options.UseSqlServer(connection));
            
            //EF MySql 配置
            services.AddDbContext<MySqlContext>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnection")));

            //EF Oracle 配置,并指定数据库版本为11G
            services.AddDbContext<MyOracleContext>(options => options.UseOracle(Configuration.GetConnectionString("OracleConnection"), b => b.UseOracleSQLCompatibility("11")));

            services.AddSwaggerGen(c =>
            {
                // swagger文档配置
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = title,
                    //Description = $"{title} HTTP API " + v,
                    //Contact = new OpenApiContact { Name = "Contact", Email = "xx@xxx.xx", Url = new Uri("https://www.cnblogs.com/straycats/") },
                    //License = new OpenApiLicense { Name = "License", Url = new Uri("https://www.cnblogs.com/straycats/") }
                });

                // 接口排序
                c.OrderActionsBy(o => o.RelativePath);

                // 配置xml文档
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // 安全校验
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // 开启oauth2安全描述
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var title = "myApi";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();

            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", title);//注意这里的v1是根据上面的version来填的
            });

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
