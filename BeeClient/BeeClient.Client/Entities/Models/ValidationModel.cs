using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
namespace BeeClient.Client.Entities.Models;

public class ValidationModel
{
    public string details { get; set; }
    public List<string> email { get; set; }
    public List<string> password { get; set; }
    
    public static ValidationModel? GetValidation(Error error)
    {
        ValidationModel? model = JsonConvert.DeserializeObject<ValidationModel>(error.Content);
        return model;
    }

    public IEnumerable<string> GetAllErrors()
    {
        var errors = new List<string>
        {
            details
        };
        if(email != null)
            errors.AddRange(email);
        if(password != null)
            errors.AddRange(password);
        //PropertyInfo[] props = GetType().GetProperties(BindingFlags.Public);




        //var errors = props.Where(x => x.GetValue(this) != null && x.PropertyType == typeof(string))
        //    .Select(x => x.GetValue(this).ToString());

        //(List<string>)(props.Where(x => x.GetValue(this) != null && x.PropertyType != typeof(string))
        //    .Select(x => x.GetValue(this)));
        return errors;
    }


}

public class CompanyError
{
    public string Detail { get; set; }
}
