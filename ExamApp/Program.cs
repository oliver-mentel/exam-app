using System;
using ExamApp.Context;
using ExamApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExamApp;

public class Program
{
    private readonly IConfiguration _configuration;
    public Program(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public static void Main(string[] args)
    {
        // Uncomment for dev
        var db = new MainContext();

        for (var i = 0; i < 10; i++)
        {
            db.Languages.Add(new Language(Guid.NewGuid(), $"Lang {i}"));
        }

        db.SaveChanges();

        CreateApp(args).Run();
    }

    public static WebApplication CreateApp(string[] args)
    {

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddTransient<IStudentsService, StudentsService>();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.Map("languages", (LanguageService service) => service.GetLanguages());

        return app;
    }
}
