```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6 (24G84) [Darwin 24.6.0]
Apple M2, 1 CPU, 8 logical and 8 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD


```
| Method           | Input | Mean         | Error     | StdDev    | Gen0   | Allocated |
|----------------- |------ |-------------:|----------:|----------:|-------:|----------:|
| **Encode**           | **100**   |    **182.75 ns** |  **0.973 ns** |  **0.812 ns** | **0.0687** |     **576 B** |
| Decode           | 100   |     75.39 ns |  0.660 ns |  0.551 ns | 0.0535 |     448 B |
| EncodeWithBuffer | 100   |    152.58 ns |  0.443 ns |  0.346 ns |      - |         - |
| **Encode**           | **1000**  |  **1,770.55 ns** | **24.659 ns** | **23.066 ns** | **0.6046** |    **5072 B** |
| Decode           | 1000  |    689.96 ns |  1.892 ns |  1.477 ns | 0.4835 |    4048 B |
| EncodeWithBuffer | 1000  |  1,529.77 ns |  6.738 ns |  5.626 ns |      - |         - |
| **Encode**           | **10000** | **16,851.99 ns** | **68.750 ns** | **64.309 ns** | **5.9509** |   **50072 B** |
| Decode           | 10000 |  6,682.16 ns | 40.053 ns | 35.506 ns | 4.7684 |   40048 B |
| EncodeWithBuffer | 10000 | 15,098.93 ns | 33.595 ns | 26.229 ns |      - |         - |
