using DotNetEnv;

namespace KpiAlumni.Utils;

public class EnvOperation
{
    public static string GetConnectionString()
    {
        Env.Load();
        return Environment.GetEnvironmentVariable("DATABASE_URL") ?? "";
    }
}