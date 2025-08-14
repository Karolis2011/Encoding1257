using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Encoding1257;

BenchmarkRunner.Run<Encoding1257Benchmark>();

[MemoryDiagnoser]
public class Encoding1257Benchmark
{
    // https://lt.wikipedia.org/wiki/Vilnius
    const string STRING_TO_BENCHMARK = "Vilnius yra vandenvardinis vietovardis - jam vardą davusi Vilnios upė, tekanti pro miestą. Upėvardis Vilnia sietinas su bendriniu žodžiu vilnia (bendrinėje kalboje įsigalėjęs variantas vilnis). Senoji Vilniaus vardo forma Vilnia žinoma rytų Lietuvos tarmėse.";

    private byte[] _inputBytes = [];
    private string _inputString = string.Empty;
    private char[] _inputChars = [];
    private byte[] _outputBytesBuffer = [];
    private char[] _outputCharsBuffer = [];

    [Params(100, 1000, 10000)]
    public int Input { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var bytes = Windows1257.Instance.GetBytes(STRING_TO_BENCHMARK);
        var builder = new StringBuilder();

        // Generate repeating input of specified length
        _inputBytes = new byte[Input];
        for (int i = 0; i < Input; i++)
        {
            _inputBytes[i] = bytes[i % bytes.Length];
        }

        while (builder.Length < Input)
        {
            builder.Append(STRING_TO_BENCHMARK);
        }
        _inputString = builder.ToString().Substring(0, Input);
        _inputChars = _inputString.ToCharArray();
        // Worst case, 1 char = 1 byte for single-byte encodings
        _outputBytesBuffer = new byte[_inputChars.Length];
        _outputCharsBuffer = new char[_inputChars.Length];
    }

    [Benchmark]
    public byte[] Encode() => Windows1257.Instance.GetBytes(_inputString);

    [Benchmark]
    public string Decode() => Windows1257.Instance.GetString(_inputBytes);

    [Benchmark]
    public int EncodeWithBuffer() =>
        Windows1257.Instance.GetBytes(_inputChars, 0, _inputChars.Length, _outputBytesBuffer, 0);

    [Benchmark]
    public int DecodeWithBuffer() =>
        Windows1257.Instance.GetChars(_inputBytes, 0, _inputBytes.Length, _outputCharsBuffer, 0);
}