using BeeClient.Client.Entities.Models;
using BeeClient.Client.Extensions;
using BeeClient.Client.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BeeClient.Client.Data;

public class DataSender
{
    private readonly HttpClient client;

    private readonly LogWriter logWriter;
    private readonly ILogger<DataSender> logger;

    public DataSender(HttpClient client, LogWriter logWriter, ILogger<DataSender> logger)
    {
        this.client = client;
        this.logWriter = logWriter;
        this.logger = logger;
    }


    //private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


    public async Task<Result<TRes>> Post<TRes, TData>(TData info, string url)
    {
        string json = JsonConvert.SerializeObject(info);


        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        logger.LogInformation(url);
        logger.LogInformation(JsonConvert.SerializeObject(data) + json);
        HttpResponseMessage response;
        try
        {
            response = await client.PostAsync(url, data);
            await logWriter.WriteToConsole(response);
            logger.LogInformation(JsonConvert.SerializeObject(response));
        }
        catch (Exception ex)
        {
            await logWriter.WriteToConsole(ex);
            logger.LogInformation(JsonConvert.SerializeObject(ex));
            throw;
        }

        string result = await response.Content.ReadAsStringAsync();


        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<TRes>(result);
        }

        await logWriter.WriteToConsole(result);
        return Result.Failure<TRes>(await Error.Create(response));
    }


    public async Task<Result<TRes>> Get<TRes>(string url, string? token = null)
    {
        if (token is not null)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        logger.LogInformation(url + token ?? string.Empty);
        HttpResponseMessage response;
        try
        {
            response = await client.GetAsync(url);
            await logWriter.WriteToConsole(response);
            logger.LogInformation(JsonConvert.SerializeObject(response));
        }
        catch (Exception ex)
        {
            await logWriter.WriteToConsole(ex);
            logger.LogInformation(JsonConvert.SerializeObject(ex));
            throw;
        }

        logger.LogInformation(url);
        string result = await response.Content.ReadAsStringAsync();
        logger.LogInformation(JsonConvert.SerializeObject(response));

        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<TRes>(result);
        }

        await logWriter.WriteToConsole(result);
        return Result.Failure<TRes>(await Error.Create(response));
    }


    //private async Task ManageToken(TokenModel tokens)
    //{
    //    if(tokens is null)
    //    {

    //    }
    //    else
    //    {
    //        if (!JwtTokenHelper.IsTokenExpired(tokens.access))
    //        {
    //        }
    //        else if (!JwtTokenHelper.IsTokenExpired(tokens.refresh))
    //        {
    //            var res = await Post<string, dynamic>(new { tokens.refresh }, "token/refresh/");
    //            tokens.access = res;
    //        }
    //        else
    //        {

    //        }
    //        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.access);
    //    }
    //}
}
