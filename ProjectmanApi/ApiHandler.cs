using ProjectmanApi.Abstraction;
using System.Text;

namespace ProjectmanApi;
/// <summary>
/// Handler zapytań do API - Tutaj są wysyłane zapytania do API
/// </summary>
public class ApiHandler
{
    public string HandleRequest(IDictionary<string, string> keyValuePairs, string type = "ping")
    {
        string token = "";
        keyValuePairs.TryGetValue("token", out token);
        try
        {
            string.IsNullOrEmpty(token);
        }
        catch (Exception)
        {
            return new ApiError((int)ApiRequestError.AccessDenied, "Token Invalid: Is null of empty").ToJSON();
        }
        switch (type)
        {
            case "ping":
                
                return new ApiPing(token).ToJSON();
            default:
                throw new ArgumentNullException("Action not exist");
        }

        //Thank you from reading
        
    } 
}
