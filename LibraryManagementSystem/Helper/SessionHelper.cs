using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class SessionHelper
{
    private const string SessionKey = "LoggedInUser";

    public static void SetUser(HttpContext httpContext, VmLoginResponse user)
    {
        var json = JsonConvert.SerializeObject(user);
        httpContext.Session.SetString(SessionKey, json);
    }

    public static VmLoginResponse GetUser(HttpContext httpContext)
    {
        var json = httpContext.Session.GetString(SessionKey);
        return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<VmLoginResponse>(json);
    }

    public static void ClearUser(HttpContext httpContext)
    {
        httpContext.Session.Remove(SessionKey);
    }
}