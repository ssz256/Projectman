using Newtonsoft.Json;

namespace ProjectmanApi.Abstraction;
/// <summary>
/// Baza objektu zwracanego przez API
/// </summary>
public abstract class ApiReturnedObject
{   /// <summary>
    /// Token
    /// </summary>
    protected string _token;

    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="token">Token</param>
    /// <exception cref="ArgumentNullException">Błąd w tokenie</exception>
    /// <exception cref="ArgumentException">Błąd w tokenie</exception>
    public ApiReturnedObject(string token)
    {
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("Token is null or empty");
        if (token.Contains(" ")) throw new ArgumentException("Token have the space");

        if (token == "errtok") goto token_err_checked;


        if (token.Length != 12) throw new ArgumentException("Token must have 12 chars");

        string[] tokens;

        Dictionary<string, string[]>? tables = JsonConvert.DeserializeObject<Dictionary<string, string[]>?>(File.ReadAllText("data/strings_tables.json"));

        tables.TryGetValue("tokens", out tokens);

        if (!tokens.Contains(token.ToUpper())) 
            throw new ArgumentException("Token not exist in DB");

token_err_checked:

        _token = token;

    }

    /// <summary>
    /// Konwersja DO Json
    /// </summary>
    /// <returns>JSON</returns>
    public string ToJSON()
    {
        return JsonConvert.SerializeObject(this);
    }
}
