using BeeClient.Client.Entities.Models.DTos;
using BeeClient.Client.Entities.Models;
using Blazored.LocalStorage;
using System.Net;
using System.Reflection;
using BeeClient.Client.Extensions;


namespace BeeClient.Client.Data;

public class CompanyData
{
    private readonly static string address = "8081/api/companies/";
    private readonly DataSender sender;
    private readonly string ServerAddres;

    public CompanyData(DataSender sender, string ServerAddres)
    {
        this.sender = sender;
        this.ServerAddres = ServerAddres;
    }
    public async Task<Result<Company>> RegisterAsync(CreateCompany company)
    {
        // Выполняем POST-запрос на сервер для регистрации компании
        return await sender.Post<Company, CreateCompany>(company, ServerAddres + address + "new");
    }
    
}



// для превращения json в класс можешь использовать например https://json2csharp.com/
public class Company
{
    public string Name { get; set; }
    public string WorkType1 { get; set; }
    public string WorkType2 { get; set; }
    public string Description { get; set; }
    public string INN { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Adress { get; set; }

}
