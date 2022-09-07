public static class Utils
{
    public static string ExecutableBaseFolder =>
    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        ?? throw new ArgumentNullException(nameof(ExecutableBaseFolder));
}