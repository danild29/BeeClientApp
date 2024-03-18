using BeeClient.Client.Data;
using BeeClient.Client.Extensions;
using BeeClient.Client.Helpers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Serilog.Configuration;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);


        //di
        builder.Services.ConfigureCommonServices();

        builder.Services.AddBlazoredLocalStorage();
        var app = builder.Build();


        await app.RunAsync();
    }
}


/// <summary>
/// методы расширения для логирования.
/// </summary>
public static class LoggerExttensions
{
    public static Serilog.ILogger Logger { get; set; }

    /// <summary>
    /// задать в директори и шаблон для записи логов.
    /// </summary>
    /// <param name="sinkConfig">sink конфигурация.</param>
    /// <param name="enviroment">enviroment</param>
    /// <param name="level">уровень логов</param>
    /// <param name="date">дата записи лога.</param>
    /// <param name="directory">директория для записи логов.</param>
    /// <returns></returns>
    public static LoggerConfiguration GetTemplate(this LoggerSinkConfiguration sinkConfig, string enviroment, string level, DateTime date, string directory)
    {
        return sinkConfig.File(
                        path: Path.Combine(directory, "serilog", $"{date:yyyy-MM-dd}", $"{level}-{enviroment}-.log"),
                        rollingInterval: RollingInterval.Hour,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}) {Message:lj}{NewLine}{Exception}",
                        shared: true);
    }
}