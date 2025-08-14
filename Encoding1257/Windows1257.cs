using System.Runtime.CompilerServices;
using System.Text;

namespace Encoding1257;


/// <summary>
/// Provides Windows-1257 (Baltic) encoding support.
/// </summary>
[Generated.Generated1257ToUnicodeMap("byteToCharMap")]
public sealed partial class Windows1257 : Encoding
{
    private static readonly Lazy<Windows1257> _instance = new(() => new Windows1257(), LazyThreadSafetyMode.ExecutionAndPublication);
    public static Windows1257 Instance => _instance.Value;

    private Windows1257() { }

    [Generated.GeneratedUnicodeTo1257Mapper]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static partial byte GetBytePartial(char c);

    /// <summary>
    /// Converts a Unicode character to a Windows-1257 byte.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte GetByte(char chr)
    {
        var b = GetBytePartial(chr);
        return b == 0 ? (byte)chr : b;
    }

    /// <summary>
    /// Converts a Windows-1257 byte to a Unicode character.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static char GetChar(byte b)
    {
        return byteToCharMap[b];
    }

    /// <inheritdoc/>
    public override int GetByteCount(char[] chars, int index, int count)
    {
        if (chars is null) throw new ArgumentNullException(nameof(chars));
        if (index < 0 || count < 0 || index + count > chars.Length) throw new ArgumentOutOfRangeException();
        return count;
    }

    /// <inheritdoc/>
    public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
        if (chars is null) throw new ArgumentNullException(nameof(chars));
        if (bytes is null) throw new ArgumentNullException(nameof(bytes));
        if (charIndex < 0 || charCount < 0 || charIndex + charCount > chars.Length) throw new ArgumentOutOfRangeException();
        if (byteIndex < 0 || byteIndex + charCount > bytes.Length) throw new ArgumentOutOfRangeException();
        for (int i = 0; i < charCount; i++)
            bytes[byteIndex + i] = GetByte(chars[charIndex + i]);
        return charCount;
    }

    /// <inheritdoc/>
    public override int GetCharCount(byte[] bytes, int index, int count)
    {
        if (bytes is null) throw new ArgumentNullException(nameof(bytes));
        if (index < 0 || count < 0 || index + count > bytes.Length) throw new ArgumentOutOfRangeException(nameof(index));
        return count;
    }

    /// <inheritdoc/>
    public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
        if (bytes is null) throw new ArgumentNullException(nameof(bytes));
        if (chars is null) throw new ArgumentNullException(nameof(chars));
        if (byteIndex < 0 || byteCount < 0 || byteIndex + byteCount > bytes.Length) throw new ArgumentOutOfRangeException(nameof(byteIndex));
        if (charIndex < 0 || charIndex + byteCount > chars.Length) throw new ArgumentOutOfRangeException(nameof(charIndex));
        for (int i = 0; i < byteCount; i++)
            chars[charIndex + i] = GetChar(bytes[byteIndex + i]);
        return byteCount;
    }

    /// <inheritdoc/>
    public override int GetMaxByteCount(int charCount) => charCount;

    /// <inheritdoc/>
    public override int GetMaxCharCount(int byteCount) => byteCount;
}
