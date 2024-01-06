using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BeeClient.Client.Helpers;

public class LogWriter
{
    public LogWriter(JavascriptHelper logger)
    {
        _logger = logger;
    }

    public bool Islogging = true;
    private readonly JavascriptHelper _logger;


    public async Task WriteToConsole(HttpResponseMessage responseMessage)
    {
        if (!Islogging) return;

        var message = JsonConvert.SerializeObject(responseMessage);
        await _logger.LogInfo(message);
    }

    public async Task WriteToConsole(string message)
    {
        if (!Islogging) return;

        await _logger.LogInfo(message);
    }
}
