using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
namespace UserApi.ProgramMain;
public class Program
{

    public static WebApplication create(String[]? args)
    {

        Console.WriteLine("############ ", args.ToString());
        // Display the number of command line arguments.
        var builder = WebApplication.CreateBuilder(args);
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Services.AddControllers();
        builder.Services.AddDbContext<UserContext>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {


            c.EnableAnnotations();
        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(
                                              "http://localhost:5000").AllowAnyHeader().AllowAnyMethod();
                      });




        });
        var app = builder.Build();

        // if (app.Environment.IsDevelopment())
        // {
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        // }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(MyAllowSpecificOrigins);
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    static void Main(string[] args)
    {


        WebApplication app = create(args);
        app.Run();


    }
}
