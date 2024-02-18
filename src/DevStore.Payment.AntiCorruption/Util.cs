namespace DevStore.Payment.AntiCorruption;

internal static class Util
{
    public static string GetRandomString()
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
}
