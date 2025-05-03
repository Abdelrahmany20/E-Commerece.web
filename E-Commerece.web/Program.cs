
using Abstraction;
using Domain.Contracts;
using E_Commerece.web.CustomMiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Shared.ErrorModels;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace E_Commerece.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                           
            #region DI Container Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDBContext>(options =>
            {

                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);

            });



            builder.Services.AddScoped<IDbInializer, DbInializer>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyRefrences).Assembly);
            builder.Services.AddScoped<IServicesManger, ServicesManger>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>

            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var Errors = context.ModelState
                        .Where(M => M.Value.Errors.Any())
                        .Select(M => new ValidationError()
                        {

                            Field = M.Key,
                            Errors = M.Value.Errors.Select(e => e.ErrorMessage)



                        });
                      
                    var Response = new ValidationErrorToReturn()
                    {

                        ValidationErrors = Errors,

                    };
                    return new BadRequestObjectResult(Response);
                };

            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"));
            });

            #endregion

            var app = builder.Build();

            await InailizeDbAsync(app);

            #region MiddleWares- Configure PipeLines
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.UseMiddleware<CustomExceptionMiddleware>();
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            #endregion
            app.Run();
        }

        public static async Task InailizeDbAsync (WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInializer>();
            await dbInitializer.InializeAsunc();


        }
    }
}
