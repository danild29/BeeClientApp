using BeeClient.Client.Data;
using BeeClient.Client.Helpers;
using Blazored.LocalStorage;
using Serilog;
using Serilog.Configuration;

namespace BeeClient.Client.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCommonServices(this IServiceCollection services)
    {
        services.AddScoped(sp => new HttpClient());
        services.AddScoped<IAlertService, AlertService>();

        services.AddScoped<JavascriptHelper>();
        services.AddScoped<LogWriter>();
        services.AddScoped<DataSender>();
        services.AddScoped<TokenData>();
        services.AddScoped<UserData>();
        services.AddScoped<CompanyData>();

        services.AddBlazoredLocalStorage();
    }
}

/// <summary>
/// методы расширения для логирования.
/// </summary>
public static class LoggerExttensions
{


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
        string folder = "serilog";

        return sinkConfig.File(
                        path: Path.Combine(directory, folder, $"{date:yyyy-MM-dd}", $"{level}-{enviroment}-.log"),
                        rollingInterval: RollingInterval.Hour,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}) {Message:lj}{NewLine}{Exception}",
                        shared: true);
    }
}