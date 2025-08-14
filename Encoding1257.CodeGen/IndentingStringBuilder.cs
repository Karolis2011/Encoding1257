using System.Diagnostics;
using System.Text;

namespace Encoding1257.CodeGen;

internal sealed class IndentingStringBuilder
{
    public static readonly char[] s_newLineChars = "\r\n\f\u0085\u2028\u2029".ToCharArray();

    private readonly StringBuilder builder;
    private readonly string indentation;

    private int currentIndentationLevel = 0;
    private string currentIndentationString = "";

    private string[] indentationStrings = new string[4];

    public IndentingStringBuilder(StringBuilder builder, string indentation = "    ")
    {
        this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        this.indentation = indentation ?? throw new ArgumentNullException(nameof(indentation));

        indentationStrings[0] = "";
        for (int i = 1, n = indentationStrings.Length; i < n; i++)
            indentationStrings[i] = indentationStrings[i - 1] + indentation;
    }

    public IndentingStringBuilder(StringBuilder builder)
        : this(builder, "    ")
    {
    }

    /// <summary>
    /// Increases the current indentation level, increasing the amount of indentation written at the start of a new line when content is written to it.
    /// </summary>
    public void IncreaseIndent()
    {
        currentIndentationLevel++;
        if (currentIndentationLevel == indentationStrings.Length)
            Array.Resize(ref indentationStrings, indentationStrings.Length * 2);

        indentationStrings[currentIndentationLevel] ??= indentationStrings[currentIndentationLevel - 1] + indentation;
        currentIndentationString = indentationStrings[currentIndentationLevel];
    }

    /// <summary>
    /// Decreases the current indentation level, decreasing the amount of indentation written at the start of a new line when content is written to it.
    /// </summary>
    public void DecreaseIndent() {
        Debug.Assert(currentIndentationLevel > 0);
        currentIndentationLevel--;
        currentIndentationString = indentationStrings[currentIndentationLevel];
    }

    /// <summary>
    /// Appends a simple end of line to the underlying buffer. No indentation is written prior to the end of line.
    /// </summary>
    public void AppendLine(string endOfLine = "\r\n") => builder.Append(endOfLine);

    /// <summary>
    /// Appends content to the underlying buffer. If the buffer is at the start of a line, then indentation will be /// appended first before the content. By default, for performance reasons, the content is assumed to contain no /// newlines in it. If the content may contain newlines, then should be passed in for 
    /// This will cause the provided content to be split into multiple lines.
    /// </summary>
    public void AppendContent(string content)
    {
        if (string.IsNullOrEmpty(content))
            return;

        // Split on all standard newlines, preserving empty lines
        var lines = content.Split(s_newLineChars, StringSplitOptions.None);

        for (int i = 0; i < lines.Length; i++)
        {
            if (builder.Length == 0 || s_newLineChars.Contains(builder[builder.Length - 1]))
                builder.Append(currentIndentationString);

            builder.Append(lines[i]);

            // Append a newline if this is not the last line
            if (i < lines.Length - 1)
                builder.Append("\r\n");
        }
    }
}

