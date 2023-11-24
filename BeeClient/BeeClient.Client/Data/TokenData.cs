using BeeClient.Client.Data.Logs;
using BeeClient.Client.Entities.Models;
using Newtonsoft.Json;
using System.Text;

namespace BeeClient.Client.Data;

public class TokenData
{
    private readonly static string address = "token/";
    private readonly DataSender _sender;



    //private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public TokenData(DataSender sender)
    {
        _sender = sender;
    }


    public async Task<string> RefreshToken(string refresh)
    {
        return await _sender.Post<string, dynamic>(new { refresh }, address + "refresh/");
    }
    public async Task<TokenModel> GetToken(UserAccount account)
    {
        return await _sender.Post<TokenModel, dynamic>(account, address);
    }



    
}

