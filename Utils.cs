using System;
using System.IO;
using System.Text;

namespace EvolutionTask;

public static class Utils
{
    public static string ExecutableBaseFolder =>
    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        ?? throw new ArgumentNullException(nameof(ExecutableBaseFolder));

    public static string ObjectPropertiesValuesToString(object obj)
    {
        return obj.GetType().GetProperties()
                        .Select(info => (info.Name, Value: info.GetValue(obj, null) ?? "(null)"))
                        .Aggregate(
                            new StringBuilder(),
                            (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                            sb => sb.ToString());
    }
}