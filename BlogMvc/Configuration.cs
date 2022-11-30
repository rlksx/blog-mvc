namespace BlogMvc;

public static class Configuration
{
    /* Json Web Token - autenticar o acesso á api */
    public static string JwtKey = "3HgJCb3ma4DZAXCHqZaZ"; //=> admin key
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_LlOTevf/wOkh";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}