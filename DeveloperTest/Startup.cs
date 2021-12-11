using DeveloperTest.Application;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using DeveloperTest.Infrastructure.Storage;
using DeveloperTest.Infrastructure.Storage.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeveloperTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "DeveloperTest";
                };
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ICreateCustomerApplicationService, CreateCustomerApplicationService>();
            services.AddTransient<ICreateJobApplicationService, CreateJobApplicationService>();
            services.AddTransient<IGetAllCustomersApplicationService, GetAllCustomersApplicationService>();
            services.AddTransient<IGetAllJobsApplicationService, GetAllJobsApplicationService>();
            services.AddTransient<IGetCustomerApplicationService, GetCustomerApplicationService>();
            services.AddTransient<IGetJobApplicationService, GetJobApplicationService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Register the Swagger generator and the Swagger UI middlewares
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
