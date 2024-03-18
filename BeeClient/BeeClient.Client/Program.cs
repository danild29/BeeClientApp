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
/// ������ ���������� ��� �����������.
/// </summary>
public static class LoggerExttensions
{
    public static Serilog.ILogger Logger { get; set; }

    /// <summary>
    /// ������ � ��������� � ������ ��� ������ �����.
    /// </summary>
    /// <param name="sinkConfig">sink ������������.</param>
    /// <param name="enviroment">enviroment</param>
    /// <param name="level">������� �����</param>
    /// <param name="date">���� ������ ����.</param>
    /// <param name="directory">���������� ��� ������ �����.</param>
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