

using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PaySpace.Calculator.Web.Services;


public class Constants
{
    public const string SessioKey = "LoginUser";
    public const string CookieSettings = "CookieSettings";
    public const string LoginUrl = "/Home/Index";
    public const string RedirectUrl = "/Calculator/Index";
    
    

}

public static class ExtensionMethod
{

    public static List<string> ModelStateError(this ModelStateDictionary modelStateDictionary)
    {
        List<string> errorList = new();

        foreach (var item in modelStateDictionary)
        {
            var re = item.Value.Errors.Select(p => p.ErrorMessage);
            errorList.AddRange(re);
        }

        return errorList;
    }

    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }

}