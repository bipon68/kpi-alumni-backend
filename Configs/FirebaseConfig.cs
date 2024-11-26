using DotNetEnv;

namespace KpiAlumni.Configs;

public class FirebaseConfig
{
    public static string Type { get; set; } = "";
    public static string ApiKey { get; private set; } = "";
    public static string ProjectId { get; set; } = "";
    public static string PrivateKeyId { get; set; } = "";
    public static string PrivateKey { get; set; } = "";
    public static string ClientEmail { get; set; } = "";
    public static string ClientId { get; set; } = "";
    public static string AuthUri { get; set; } = "";
    public static string TokenUri { get; set; } = "";
    public static string AuthProviderX509CertUrl { get; set; } = "";
    public static string ClientX509CertUrl { get; set; } = "";
    public static string UniverseDomain { get; set; } = "";

    public static void Load()
    {
        Env.Load();
        Type = Environment.GetEnvironmentVariable("FIREBASE_TYPE") ?? "service_account";
        ApiKey = Environment.GetEnvironmentVariable("FIREBASE_API_KEY") ?? "";
        ProjectId = Environment.GetEnvironmentVariable("FIREBASE_PROJECT_ID") ?? "";
        PrivateKeyId = Environment.GetEnvironmentVariable("FIREBASE_PRIVATE_KEY_ID") ?? "";
        PrivateKey = Environment.GetEnvironmentVariable("FIREBASE_PRIVATE_KEY") ?? "";
        ClientEmail = Environment.GetEnvironmentVariable("FIREBASE_CLIENT_EMAIL") ?? "";
        ClientId = Environment.GetEnvironmentVariable("FIREBASE_CLIENT_ID") ?? "";
        AuthUri = Environment.GetEnvironmentVariable("FIREBASE_AUTH_URI") ?? "";
        TokenUri = Environment.GetEnvironmentVariable("FIREBASE_TOKEN_URI") ?? "";
        AuthProviderX509CertUrl = Environment.GetEnvironmentVariable("FIREBASE_AUTH_PROVIDER_X509_CERT_URL") ?? "";
        ClientX509CertUrl = Environment.GetEnvironmentVariable("FIREBASE_CLIENT_X509_CERT_URL") ?? "";
        UniverseDomain = Environment.GetEnvironmentVariable("FIREBASE_UNIVERSE_DOMAIN") ?? "";
    }

    public static string GetJson()
    {
        var json = new
        {
            type = Type,
            apiKey = ApiKey,
            projectId = ProjectId,
            privateKeyId = PrivateKeyId,
            privateKey = PrivateKey,
            clientEmail = ClientEmail,
            clientId = ClientId,
            authUri = AuthUri,
            tokenUri = TokenUri,
            authProviderX509CertUrl = AuthProviderX509CertUrl,
            clientX509CertUrl = ClientX509CertUrl,
            universeDomain = UniverseDomain
        };
        return Newtonsoft.Json.JsonConvert.SerializeObject(json);
    }
}