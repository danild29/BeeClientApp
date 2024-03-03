using BeeClient.Client.Entities.Models.DTos;
using BeeClient.Client.Entities.Models;
using System.Net;

namespace BeeClient.Client.Data
{
    public class CompanyData
    {
        private readonly DataSender sender;

        public CompanyData(DataSender sender)
        {
            this.sender = sender;
        }
        public async Task<Result<Company>> Register(CreateCompany company)
        {
            return await sender.Post<Company, CreateCompany>(company, "companies/new");
        }

    }

    // для превращения json в класс можешь использовать например https://json2csharp.com/
    public class Company
    {
        public int MyProperty { get; set; }
    }
}
