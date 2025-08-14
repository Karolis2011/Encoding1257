using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Encoding1257.CodeGen;

[Generator]
public sealed class Win1257Gen : IIncrementalGenerator
{
    private static readonly Dictionary<char, byte> encodingMap = new()
    {
        ['\u20AC'] = 0x80,
        ['\u201A'] = 0x82,
        ['\u201E'] = 0x84,
        ['\u2026'] = 0x85,
        ['\u2020'] = 0x86,
        ['\u2021'] = 0x87,
        ['\u2030'] = 0x89,
        ['\u2039'] = 0x8B,
        ['\u00A8'] = 0x8D,
        ['\u02C7'] = 0x8E,
        ['\u00B8'] = 0x8F,
        ['\u2018'] = 0x91,
        ['\u2019'] = 0x92,
        ['\u201C'] = 0x93,
        ['\u201D'] = 0x94,
        ['\u2022'] = 0x95,
        ['\u2013'] = 0x96,
        ['\u2014'] = 0x97,
        ['\u2122'] = 0x99,
        ['\u203A'] = 0x9B,
        ['\u00AF'] = 0x9D,
        ['\u02DB'] = 0x9E,
        ['\u00D8'] = 0xA8,
        ['\u0156'] = 0xAA,
        ['\u00C6'] = 0xAF,
        ['\u00F8'] = 0xB8,
        ['\u0157'] = 0xBA,
        ['\u00E6'] = 0xBF,
        ['\u0104'] = 0xC0,
        ['\u012E'] = 0xC1,
        ['\u0100'] = 0xC2,
        ['\u0106'] = 0xC3,
        ['\u0118'] = 0xC6,
        ['\u0112'] = 0xC7,
        ['\u010C'] = 0xC8,
        ['\u0179'] = 0xCA,
        ['\u0116'] = 0xCB,
        ['\u0122'] = 0xCC,
        ['\u0136'] = 0xCD,
        ['\u012A'] = 0xCE,
        ['\u013B'] = 0xCF,
        ['\u0160'] = 0xD0,
        ['\u0143'] = 0xD1,
        ['\u0145'] = 0xD2,
        ['\u014C'] = 0xD4,
        ['\u0172'] = 0xD8,
        ['\u0141'] = 0xD9,
        ['\u015A'] = 0xDA,
        ['\u016A'] = 0xDB,
        ['\u017B'] = 0xDD,
        ['\u017D'] = 0xDE,
        ['\u0105'] = 0xE0,
        ['\u012F'] = 0xE1,
        ['\u0101'] = 0xE2,
        ['\u0107'] = 0xE3,
        ['\u0119'] = 0xE6,
        ['\u0113'] = 0xE7,
        ['\u010D'] = 0xE8,
        ['\u017A'] = 0xEA,
        ['\u0117'] = 0xEB,
        ['\u0123'] = 0xEC,
        ['\u0137'] = 0xED,
        ['\u012B'] = 0xEE,
        ['\u013C'] = 0xEF,
        ['\u0161'] = 0xF0,
        ['\u0144'] = 0xF1,
        ['\u0146'] = 0xF2,
        ['\u014D'] = 0xF4,
        ['\u0173'] = 0xF8,
        ['\u0142'] = 0xF9,
        ['\u015B'] = 0xFA,
        ['\u016B'] = 0xFB,
        ['\u017C'] = 0xFD,
        ['\u017E'] = 0xFE,
        ['\u02D9'] = 0xFF
    };


    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static (postInitializationContext) =>
        {
            postInitializationContext.AddEmbeddedAttributeDefinition();
            postInitializationContext.AddSource("Win1257GenAttributes.g.cs", SourceText.From("""
            // Auto generated file. DO NOT EDIT

            using Microsoft.CodeAnalysis;

            namespace Generated {
                /// <summary>
                /// Attribute to mark classes to receive the generated 1257 to Unicode map.
                /// </summary>
                [global::System.AttributeUsage(global::System.AttributeTargets.Class), global::Microsoft.CodeAnalysis.Embedded]
                public sealed class Generated1257ToUnicodeMapAttribute : global::System.Attribute
                {
                    public Generated1257ToUnicodeMapAttribute(string fieldName)
                    {
                        FieldName = fieldName;
                    }

                    public string FieldName { get; }
                }

                [global::System.AttributeUsage(global::System.AttributeTargets.Method), global::Microsoft.CodeAnalysis.Embedded]
                public sealed class GeneratedUnicodeTo1257MapperAttribute : global::System.Attribute
                {
                }

            }
            """, Encoding.UTF8));
        });

        var gen1257ToUnicodeMapPipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
            fullyQualifiedMetadataName: "Generated.Generated1257ToUnicodeMapAttribute",
            predicate: static (node, _) => node is ClassDeclarationSyntax,
            transform: static (context, _) =>
            {
                if (context.TargetSymbol is not INamedTypeSymbol classSymbol)
                    throw new ArgumentException("Expected attribute on a class symbol.", nameof(context));
                if (classSymbol.ContainingSymbol is not INamespaceSymbol namespaceSymbol)
                    throw new ArgumentException("Expected class to be in a namespace.", nameof(context));

                // we locate our attribute
                var attributes = classSymbol.GetAttributes()
                    .Where(attr => attr.AttributeClass?.ToDisplayString() == "Generated.Generated1257ToUnicodeMapAttribute")
                    .ToImmutableArray();
                if (attributes.IsEmpty)
                    throw new ArgumentException("Expected attribute not found.", nameof(context));

                var fieldName = attributes.FirstOrDefault()?.ConstructorArguments.FirstOrDefault().Value as string;
                fieldName ??= "ToUnicodeMap";
                return new ToUnicodeModel
                {
                    Namespace = namespaceSymbol.Name,
                    ClassName = classSymbol.Name,
                    FieldName = fieldName,
                    DeclaredAccessibility = classSymbol.DeclaredAccessibility
                };
            }
        );

        context.RegisterSourceOutput(gen1257ToUnicodeMapPipeline, static (context, model) =>
        {
            var toUnicodeMap = new char[256];
            for (int i = 0; i < 256; i++)
            {
                toUnicodeMap[i] = (char)i;
            }
            // Applying overrides from encoding map.
            foreach (var kvp in encodingMap)
            {
                toUnicodeMap[kvp.Value] = kvp.Key;
            }

            var sourceText = SourceText.From($$"""
            // Auto generated file. DO NOT EDIT

            using System.Text;
    
            namespace {{model.Namespace}};

            {{AccessibilityToString(model.DeclaredAccessibility)}} partial class {{model.ClassName}} {
                /// <summary>
                /// The mapping from Windows-1257 bytes to Unicode characters.
                /// </summary>
                internal static readonly char[] {{model.FieldName}} = new char[256] {
                    {{string.Join(", ", toUnicodeMap.Select(c => $"'{CharToCodeRepr(c)}'"))}}
                };
            }
            """, Encoding.UTF8);

            context.AddSource($"{model.ClassName}_{model.FieldName}.g.cs", sourceText);
        });

        var genUnicodeTo1257MapperPipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
            fullyQualifiedMetadataName: "Generated.GeneratedUnicodeTo1257MapperAttribute",
            predicate: static (node, _) => node is MethodDeclarationSyntax,
            transform: static (context, _) =>
            {
                if (context.TargetSymbol is not IMethodSymbol methodSymbol)
                    throw new ArgumentException("Expected attribute on a method symbol.", nameof(context));
                if (methodSymbol.ContainingSymbol is not INamedTypeSymbol classSymbol)
                    throw new ArgumentException("Expected method to be in a class.", nameof(context));
                if (classSymbol.ContainingSymbol is not INamespaceSymbol namespaceSymbol)
                    throw new ArgumentException("Expected class to be in a namespace.", nameof(context));


                return new FromUnicodeModel
                {
                    Namespace = namespaceSymbol.Name,
                    ClassName = classSymbol.Name,
                    MethodName = methodSymbol.Name,
                    MethodAccessibility = methodSymbol.DeclaredAccessibility,
                    ClassAccessibility = classSymbol.DeclaredAccessibility
                };
            }
        );

        context.RegisterSourceOutput(genUnicodeTo1257MapperPipeline, static (context, model) =>
        {
            var outputBuffer = new StringBuilder();
            var output = new IndentingStringBuilder(outputBuffer);
            output.AppendContent($$"""
            // Auto generated file. DO NOT EDIT
            using System.Text;

            namespace {{model.Namespace}};
            
            {{AccessibilityToString(model.ClassAccessibility)}} partial class {{model.ClassName}}
            {
            """);
            output.AppendLine();
            output.IncreaseIndent();

            output.AppendContent($$"""
            {{AccessibilityToString(model.MethodAccessibility)}} static partial byte {{model.MethodName}}(char c)
            {
            """);
            output.AppendLine();
            output.IncreaseIndent();

            output.AppendContent("""
            switch(c) {
            """);
            output.AppendLine();
            output.IncreaseIndent();
            foreach (var kvp in encodingMap)
            {
                output.AppendContent($$"""
                case '{{CharToCodeRepr(kvp.Key)}}':
                    return 0x{{kvp.Value:X2}};
                """);
                output.AppendLine();
            }

            output.DecreaseIndent();
            output.AppendContent($$"""
                default:
                    return 0x00;
            }
            """);
            output.DecreaseIndent();
            output.AppendLine();
            output.AppendContent("""
            }
            """);
            output.AppendLine();
            output.DecreaseIndent();
            output.AppendContent("""
            }
            """);
            output.AppendLine();


            context.AddSource($"{model.ClassName}_{model.MethodName}.g.cs", SourceText.From(outputBuffer.ToString(), Encoding.UTF8));
        });
    }

    private static string CharToCodeRepr(char c)
    {
        //     // Non printable ASCII characters
        //     if (c < 0x30)
        //         return $"\\u{((int)c).ToString("X4")}";

        //     // Extended ASCII characters
        //     if (c > 0x7E && c < 0xFF)
        //         return $"\\u{((int)c).ToString("X4")}";

        //     // Other characters
        //     if (c > 0xFF)
        //         return $"\\u{((int)c).ToString("X4")}";

        return $"\\u{((int)c).ToString("X4")}";
    }

    private static string AccessibilityToString(Accessibility accessibility)
    {
        return accessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Private => "private",
            Accessibility.Protected => "protected",
            Accessibility.Internal => "internal",
            _ => "private"
        };
    }

    private struct ToUnicodeModel
    {
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string FieldName { get; set; }
        public Accessibility DeclaredAccessibility { get; set; }
    }

    private struct FromUnicodeModel
    {
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public Accessibility ClassAccessibility { get; set; }
        public Accessibility MethodAccessibility { get; set; }
    }
}