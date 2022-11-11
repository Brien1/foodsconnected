using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
namespace UserApi.ProgramMain;
public class Program
{

    public static WebApplication create(String[]? args) {
        
        Console.WriteLine("############ ",args.ToString());
        // Display the number of command line arguments.
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<UserContext>(opt =>
            opt.UseInMemoryDatabase("TodoList"));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

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
