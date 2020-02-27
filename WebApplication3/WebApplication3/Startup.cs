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

            //EF Core �ڴ������ݿ�
            services.AddDbContext<MyContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            
            //SQL Server 2012 ���ϰ汾
            var connection = @"Server=127.0.0.1;User Id=sa;Password=ntidba;Database=MySqlServer;Persist Security Info=True";
            services.AddDbContext<MySqlServerContext>(options => options.UseSqlServer(connection));
            
            //EF MySql ����
            services.AddDbContext<MySqlContext>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnection")));

            //EF Oracle ����,��ָ�����ݿ�汾Ϊ11G
            services.AddDbContext<MyOracleContext>(options => options.UseOracle(Configuration.GetConnectionString("OracleConnection"), b => b.UseOracleSQLCompatibility("11")));

            services.AddSwaggerGen(c =>
            {
                // swagger�ĵ�����
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = title,
                    //Description = $"{title} HTTP API " + v,
                    //Contact = new OpenApiContact { Name = "Contact", Email = "xx@xxx.xx", Url = new Uri("https://www.cnblogs.com/straycats/") },
                    //License = new OpenApiLicense { Name = "License", Url = new Uri("https://www.cnblogs.com/straycats/") }
                });

                // �ӿ�����
                c.OrderActionsBy(o => o.RelativePath);

                // ����xml�ĵ�
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // ��ȫУ��
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // ����oauth2��ȫ����
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
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

            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();

            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", title);//ע�������v1�Ǹ��������version�����
            });

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
