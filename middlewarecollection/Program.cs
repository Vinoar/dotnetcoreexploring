
using MiddlewareCollections.Middleware;

namespace MiddlewareCollections
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build(); 
           
            app.UseMiddleware<ShortCircuitValidateRequestPathMiddleware>();

            app.UseMiddleware<FirstMiddleware>(Guid.NewGuid()); 

            app.UseAuthorization();

            app.UseMiddleware<SecondMiddleware>(Guid.NewGuid());

            ///
            /// MapControllers is called to map attribute routed controllers. 
            /// In the following example: HomeController matches a set of URLs similar to what the default
            /// conventional route {controller=Home}/{action=Index}/{id?} matches
            ///
            app.MapControllers();

            app.Run();

            ///
            /// The function won't be invoked because it's scheduled to run after the app.Run() method. 
            /// The rule dictates that middleware must be added before calling app.Run().
            ///
            app.UseMiddleware<LastMiddleware>(Guid.NewGuid());

        }
    }
}

 