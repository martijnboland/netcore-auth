using System.Security.Principal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreAuth.Mvc.Authorization;
using NetCoreAuth.Mvc.Models;

namespace NetCoreAuth.Mvc
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPrincipal>(sp => 
                sp.GetService<IHttpContextAccessor>().HttpContext.User
            );
            services.AddSingleton<IAuthorizationHandler, TodoOwnerHandler>();
            
            services.AddTransient<TodoStore>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCookieAuthentication(new CookieAuthenticationOptions 
            {
                AccessDeniedPath = new PathString("/Account/Forbidden/"),
            });
            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                ClientId = Configuration["AzureAD:ClientId"],
                Authority = $"https://login.microsoftonline.com/{Configuration["AzureAD:TenantId"]}",
                CallbackPath = "/signin-aad",
                SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme
            });
            app.UseMvcWithDefaultRoute();
        }
    }
}
