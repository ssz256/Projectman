using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ProjectmanWWWApp;

/// <summary>
/// Serwer
/// </summary>
public class 
Server
{
    /// <summary>
    /// HttpListener - Objekt System.Net - Służy do czekania i przechwytywania żądań
    /// </summary>
    public HttpListener Listener { get; private set; }
    
    /// <summary>
    /// HttpListener - Objekt System.Net - Służy do czekania i przechwytywania żądań
    /// </summary>
    public ApiHandler ApiHandler { get; private set; }

    /// <summary>
    /// Port serwera HTTP
    /// </summary>
    public int Port { get; private set; }

    /// <summary>
    /// Inicializowanie instancji serwera
    /// </summary>
    /// <param name="port">Port HTTP</param>
    public Server(int port = 80) 
    {
        Port = port;
        Listener = new HttpListener();
        ApiHandler = new ApiHandler();

        Listener.Prefixes.Add($"http://*:{Port}/");
    }

    /// <summary>
    /// Request Handler
    /// </summary>
    /// <returns>Async-Await</returns>
    public async Task RequestHandler() 
    {
        while (true)
        {
            HttpListenerContext ctx = await Listener.GetContextAsync();

            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse resp = ctx.Response;

            resp.ContentType = "text/html";
            resp.StatusCode = 404;



            byte[] data = IOSiteLoader.LoadFile("www/404.html");

            if (req.Url.AbsolutePath.StartsWith("/api/"))
            {
                resp.StatusCode = 404;
                resp.ContentType = "text/json";
                IDictionary<string, string> args =
                    new Dictionary<string, string>();

                byte[] data_req = { };

                StreamReader reader = new StreamReader(req.InputStream);



                args = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());


                try
                {
                    data = Encoding.UTF8.GetBytes(ApiHandler.HandleRequest(args));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    if (ex.Message == "Token not exist in DB" || ex.Message == "Value cannot be null. (Parameter 'Token is null or empty')" || ex.Message == "Token have the space" || ex.Message == "Token must have 12 chars")
                    {
                        resp.StatusCode = 403;
                        data = Encoding.UTF8.GetBytes(new ApiError((int)ApiRequestError.AccessDenied, "Token Invalid: " + ex.Message).ToJSON());
                    } else {
                        continue;
                    }
                    
                }
               
            }

            if (req.Url.AbsolutePath == "/" || req.Url.AbsolutePath.ToLower() == "/index.html")
            {
                resp.StatusCode = 200;
                data = IOSiteLoader.LoadFile("www/index.html");
            }

            if (req.Url.AbsolutePath.ToLower() == "/dark.css")
            {
                resp.StatusCode = 200;
                resp.ContentType = "text/css";
                data = File.ReadAllBytes("www/dark.css");
            }

            await resp.OutputStream.WriteAsync(data);
            
            Console.WriteLine("Request Recivied: " + req.Url.AbsolutePath);

            resp.Close();
        }
    }
}
