
using System.Text;

namespace Encoding1257;


public class Win1257EncodingProvider : EncodingProvider
{
    private static readonly Lazy<Win1257EncodingProvider> _instance = new(() => new Win1257EncodingProvider(), LazyThreadSafetyMode.ExecutionAndPublication);
    public static Win1257EncodingProvider Instance => _instance.Value;

    public const int BALTIC_CODEPAGE = 1257;
    
    public override Encoding? GetEncoding(int codepage)
    {
        if (codepage == BALTIC_CODEPAGE)
            return Windows1257.Instance;
        return null;
    }

    public override Encoding? GetEncoding(string name)
    {
        return (name?.ToLowerInvariant()) switch
        {
            "windows-1257" or "cp1257" or "iso-8859-13" => Windows1257.Instance,
            _ => null,
        };
    }
}
