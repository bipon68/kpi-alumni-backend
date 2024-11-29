using KpiAlumni.Utils;

namespace KpiAlumni.Models.Firebase;

public class FirebaseProviderNames
{
    public static KeyObject GOOOGLE { get; set; } = new KeyObject { Key = "google.com", Title = "Google" };

    public static KeyObject GITHUB { get; set; } = new KeyObject { Key = "github.com", Title = "GitHub" };

    public static KeyObject PHONE { get; set; } = new KeyObject { Key = "phone", Title = "Identity" };
    public static KeyObject EMAIL { get; set; } = new KeyObject { Key = "password", Title = "Identity" };
}