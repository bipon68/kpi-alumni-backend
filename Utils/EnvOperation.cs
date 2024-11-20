using DotNetEnv;

namespace KpiAlumni.Utils
{
    public class EnvOperation
    {
        public static string GetMode()
        {
            DotNetEnv.Env.Load();

            return Environment.GetEnvironmentVariable("MODE") ?? "DEV";
        }

        public static string GetConnectionString()
        {
            var mode = GetMode();

            var envVarName = mode switch
            {
                "DEV" => "DATABASE_URL_DEV",
                "PROD" => "DATABASE_URL_PROD",
                _ => "DATABASE_URL_DEV"
            };

            var connectionString = Environment.GetEnvironmentVariable(envVarName) ?? string.Empty;

            var output = string.IsNullOrEmpty(connectionString) ? Environment.GetEnvironmentVariable("DATABASE_URL_DEV") : connectionString;

            return output ?? string.Empty;
        }

    }
}
