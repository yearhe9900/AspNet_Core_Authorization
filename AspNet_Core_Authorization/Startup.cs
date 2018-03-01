using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNet_Core_Authorization
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
            // 使用内存存储，密钥，客户端和资源来配置身份服务器。
            services.AddIdentityServer()//注册IdentitiyServer到DI中。
                .AddDeveloperSigningCredential()//扩展在每次启动时，为令牌签名创建了一个临时密钥。在生成环境需要一个持久化的密钥。
                .AddInMemoryApiResources(Config.GetApiResources())//添加api资源
                .AddInMemoryClients(Config.GetClients())//添加客户端
                .AddResourceOwnerValidator<UserValidator>();//添加自定义用户验证
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);

            //限制公共对诊断信息的获取
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseWelcomePage();
        }
    }
}