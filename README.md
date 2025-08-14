# Encoding1257

A high-performance managed implementation of Windows-1257 (Baltic) encoding for .NET.

## Overview

Windows-1257 is a legacy character encoding used primarily for Baltic languages (Lithuanian, Latvian, Estonian). While this encoding has been largely superseded by Unicode (UTF-8/UTF-16) for modern applications, it's still needed for:

- **Legacy System Integration**: Working with older systems and databases that store data in Windows-1257
- **File Format Compatibility**: Reading and writing files created by legacy applications
- **Data Migration**: Converting historical data from Windows-1257 to modern encodings
- **Interoperability**: Communicating with systems that still use this encoding

This library provides a fast, managed implementation of the Windows-1257 encoding that integrates seamlessly with .NET's `System.Text.Encoding` infrastructure.

> **Note**: For new projects, consider using UTF-8 encoding unless you specifically need Windows-1257 compatibility.

## Features

- **High Performance**: Optimized implementation with aggressive inlining and efficient lookup tables
- **Zero-Allocation Operations**: When using buffer-based methods (`GetBytes`/`GetChars` with pre-allocated buffers), no heap allocations occur
- **Source Generation**: Uses Roslyn source generators to generate optimal encoding/decoding tables at compile time
- **Zero Dependencies**: Pure .NET implementation with no external dependencies
- **Thread Safe**: Singleton pattern ensures safe usage across multiple threads
- **Full .NET Integration**: Inherits from `System.Text.Encoding` for seamless integration

## Installation

Install the package from NuGet:

```bash
dotnet add package Encoding1257
```

## Usage

### Basic Usage

```csharp
using Encoding1257;
using System.Text;

// Get the Windows-1257 encoding instance
var encoding = Windows1257.Instance;

// Encode a string to bytes
string text = "Vilnius yra Lietuvos sostinė";
byte[] bytes = encoding.GetBytes(text);

// Decode bytes back to string
string decoded = encoding.GetString(bytes);
```

### Integration with System.Text.Encoding

```csharp
// Register with Encoding.RegisterProvider if needed
Encoding.RegisterProvider(Win1257EncodingProvider.Instance)

// Use like any other encoding
Encoding encoding = Encoding.GetEncoding(1257);
var encoder = encoding.GetEncoder();
var decoder = encoding.GetDecoder();
```

### Working with Streams

```csharp
using var stream = new FileStream("file.txt", FileMode.Open);
using var reader = new StreamReader(stream, Windows1257.Instance);
string content = reader.ReadToEnd();
```

## Architecture

This project consists of three main components:

### 1. Encoding1257 (Main Library)
- Contains the `Windows1257` encoding implementation
- Uses source generation for optimal performance
- Provides the public API

### 2. Encoding1257.CodeGen (Source Generator)
- Roslyn-based source generator
- Generates Unicode ↔ Windows-1257 mapping tables at compile time
- Creates optimized lookup methods

### 3. Encoding1257.Benchmark (Performance Testing)
- BenchmarkDotNet-based performance tests
- Measures encoding/decoding performance
- Validates performance improvements

## Performance

The library is designed for high performance with the following optimizations:

- **Compile-time code generation**: Mapping tables are generated at compile time
- **Aggressive inlining**: Critical methods use `MethodImplOptions.AggressiveInlining`
- **Efficient lookups**: Direct array indexing for byte-to-char conversion
- **Optimized Unicode mapping**: Generated switch statements for char-to-byte conversion

### Benchmark Results (Apple M2)

| Method           | Input | Mean         | Allocated |
|------------------|-------|-------------:|----------:|
| Encode           | 100   | 182.75 ns    | 576 B     |
| Decode           | 100   | 75.39 ns     | 448 B     |
| EncodeWithBuffer | 100   | 152.58 ns    | 0 B       |
| DecodeWithBuffer | 100   | 61.82 ns     | 0 B       |

*Results for .NET 9.0 on Apple M2*

## Character Support

Windows-1257 supports all standard ASCII characters (0x00-0x7F) plus additional characters for Baltic languages:

- Lithuanian characters: ą, č, ę, ė, į, š, ų, ū, ž and their uppercase variants
- Latvian characters: ā, č, ē, ģ, ī, ķ, ļ, ņ, š, ū, ž and their uppercase variants
- Estonian characters: ä, ö, ü, õ and their uppercase variants
- Common European characters: €, „, ", –, —, etc.

## Requirements

- C# 13.0 language features

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

### Development Setup

1. Clone the repository
2. Build the solution: `dotnet build`
3. Run tests: `dotnet test`
4. Run benchmarks: `dotnet run --project Encoding1257.Benchmark -c Release`

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgments

- Character mapping based on the official Windows-1257 specification
- Performance optimizations inspired by .NET's built-in encoding implementations
