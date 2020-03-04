using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Libs.Sessions;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace GFT_ClubHouse__Management {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMemoryCache();
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(120); });

            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<CookieTempDataProviderOptions>(options => { options.Cookie.IsEssential = true; });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<Session>();
            services.AddScoped<LoginAdmin>();
            services.AddScoped<LoginUser>();

            services.AddTransient<IClubHouseRepository, ClubHouseRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IMusicalGenreRepository, MusicalGenreRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();

            services.AddResponseCompression();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "CHM API",
                    Description = "Club House Management API",
                    Contact = new Contact {
                        Name = "Vinícius Henrique Santos Araújo",
                        Email = "viniciushsaraujoo@gmail.com",
                        Url = "https://linkedin.com/in/ViniciusHSAraujo",
                    }
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseResponseCompression();
            
            app.UseSwagger();

            app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("/swagger/v1/swagger.json", "CHM API V1"); });

            app.UseMvc(routes => {
                //Rota para quando há áreas.
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                //Rota padrão.
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}