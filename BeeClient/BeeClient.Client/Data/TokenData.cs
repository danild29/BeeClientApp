//using BeeClient.Client.Data.Logs;
using BeeClient.Client.Entities.Models;
using Newtonsoft.Json;
using System.Text;

namespace BeeClient.Client.Data;

public class TokenData
{
    private readonly static string address = @"http://213.171.4.235:8081/api/token/";
    private readonly DataSender _sender;



    //private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public TokenData(DataSender sender)
    {
        _sender = sender;
    }


    public async Task<Result<string>> RefreshToken(string refresh)
    {
        return await _sender.Post<string, dynamic>(new { refresh }, address + "refresh/");
    }
    public async Task<Result<TokenModel>> GetToken(UserAccount account)
    {
        return await _sender.Post<TokenModel, dynamic>(account, address);
    }



    
}

