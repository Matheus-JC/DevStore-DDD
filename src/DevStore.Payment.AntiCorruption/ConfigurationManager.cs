
namespace DevStore.Payment.AntiCorruption;

public class ConfigurationManager : IConfigurationManager
{
    public string GetValue(string node)
    {
        return Util.GetRandomString();
    }
}
