using BeeClient.Client.Entities.Models.DTos;
using BeeClient.Client.Entities.Models;
using Blazored.LocalStorage;
using System.Net;
using System.Reflection;

namespace BeeClient.Client.Data;

public class CompanyData
{
    private readonly DataSender sender;

    public CompanyData(DataSender sender)
    {
        this.sender = sender;
    }
    public async Task<Result<Company>> RegisterAsync(CreateCompany company)
    {
        // Выполняем POST-запрос на сервер для регистрации компании
        return await sender.Post<Company, CreateCompany>(company, "companies/new");
    }

}



// для превращения json в класс можешь использовать например https://json2csharp.com/
public class Company
{
    public string СompanyName { get; set; }
    public string Inn { get; set; }
    public string PhNumber { get; set; }
    public string CompanyTag { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Experience { get; set; }
    public string IsActive { get; set; }

}
