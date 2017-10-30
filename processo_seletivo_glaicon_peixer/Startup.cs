using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using processo_seletivo_glaicon_peixer.Data;
using processo_seletivo_glaicon_peixer.Mapper;

namespace processo_seletivo_glaicon_peixer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperConfiguration.Configure();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddDbContext<DataContext>(options =>
                options.UseMySql(Configuration["Data:ConnectionString"]));

            services.AddAutoMapper();
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();

            CreateDatabase(app);
        }

        private static void CreateDatabase(IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DataContext>();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
