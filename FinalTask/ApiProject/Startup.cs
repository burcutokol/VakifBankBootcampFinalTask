using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using DataProject.Context;
using Microsoft.EntityFrameworkCore;
using DataProject.Uow;
using AutoMapper;
using OperationProject.Mapper;
using MediatR;
using OperationProject.Cqrs;
using System.Reflection;

namespace ApiProject
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
            string connection = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<DbContextClass>(opt => opt.UseSqlServer(connection));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMediatR(typeof(GetAllProductsQuery).GetTypeInfo().Assembly);


            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
            services.AddSingleton(config.CreateMapper());
            services.AddMemoryCache();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalTaskApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinalTaskApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
