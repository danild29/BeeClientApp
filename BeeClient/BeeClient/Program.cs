using BeeClient.Client.Extensions;
using BeeClient.Client.Pages;
using BeeClient.Components;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

        // di
        builder.Services.ConfigureCommonServices();
        string hostingDirectory = AppDomain.CurrentDomain.BaseDirectory;

        var log = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Logger(l => l
                //.Filter.ByIncludingOnly(e => e.Level > LogEventLevel.Debug)
                .WriteTo.Map(le => le.Timestamp.Date, (date, lc) => lc.GetTemplate("serilog", "DEBUG", date, hostingDirectory)));


        Log.Logger = log.Enrich.FromLogContext().CreateLogger();
        LoggerExttensions.Logger = Log.Logger;
        builder.Logging.AddSerilog();

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        Log.Information("Log something. Please!");

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

        app.Run();
    }
}




