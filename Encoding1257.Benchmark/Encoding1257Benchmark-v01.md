```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6 (24G84) [Darwin 24.6.0]
Apple M2, 1 CPU, 8 logical and 8 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD


```
| Method           | Input | Mean        | Error     | StdDev    | Gen0   | Allocated |
|----------------- |------ |------------:|----------:|----------:|-------:|----------:|
| **Encode**           | **100**   |    **269.3 ns** |   **5.34 ns** |   **9.36 ns** | **0.0687** |     **576 B** |
| Decode           | 100   |    237.0 ns |   4.69 ns |   5.21 ns | 0.0534 |     448 B |
| EncodeWithBuffer | 100   |    237.5 ns |   4.58 ns |   6.27 ns |      - |         - |
| **Encode**           | **1000**  |  **3,404.7 ns** |  **36.94 ns** |  **32.75 ns** | **0.6027** |    **5072 B** |
| Decode           | 1000  |  3,295.8 ns |  62.81 ns |  64.50 ns | 0.4807 |    4048 B |
| EncodeWithBuffer | 1000  |  3,195.1 ns |  35.04 ns |  32.78 ns |      - |         - |
| **Encode**           | **10000** | **33,119.3 ns** | **130.42 ns** | **108.90 ns** | **5.9204** |   **50072 B** |
| Decode           | 10000 | 31,688.6 ns | 391.52 ns | 347.07 ns | 4.7607 |   40048 B |
| EncodeWithBuffer | 10000 | 31,538.3 ns |  64.62 ns |  57.28 ns |      - |         - |
