using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNet_Core_Api
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
            services.AddMvcCore().AddJsonFormatters();

            #region use IdentityServer4.AccessTokenValidation

            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, (option) =>
                {
                    option.Authority = "http://localhost:57345";//identityserver4地址
                    option.RequireHttpsMetadata = false;//使用https
                    option.ApiName = "api1";//api scope
                });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            app.Use((context, next) =>
            {
                var user = context.User;

                context.Response.StatusCode = user.Identity.IsAuthenticated ? 200 : 401;

                return next.Invoke();
            });
            app.UseMvc();
        }
    }
}
