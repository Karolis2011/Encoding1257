```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6 (24G84) [Darwin 24.6.0]
Apple M2, 1 CPU, 8 logical and 8 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD


```
| Method           | Input | Mean         | Error      | StdDev     | Gen0   | Allocated |
|----------------- |------ |-------------:|-----------:|-----------:|-------:|----------:|
| **Encode**           | **100**   |    **268.31 ns** |   **4.600 ns** |   **4.077 ns** | **0.0687** |     **576 B** |
| Decode           | 100   |     73.47 ns |   0.547 ns |   0.427 ns | 0.0535 |     448 B |
| EncodeWithBuffer | 100   |    228.50 ns |   1.070 ns |   0.949 ns |      - |         - |
| **Encode**           | **1000**  |  **3,359.59 ns** |  **15.341 ns** |  **14.350 ns** | **0.6027** |    **5072 B** |
| Decode           | 1000  |    671.24 ns |   3.226 ns |   2.860 ns | 0.4835 |    4048 B |
| EncodeWithBuffer | 1000  |  3,163.83 ns |  30.167 ns |  25.191 ns |      - |         - |
| **Encode**           | **10000** | **33,118.56 ns** | **238.437 ns** | **211.368 ns** | **5.9204** |   **50072 B** |
| Decode           | 10000 |  6,541.81 ns |  94.053 ns |  87.977 ns | 4.7684 |   40048 B |
| EncodeWithBuffer | 10000 | 31,603.14 ns | 131.058 ns | 109.439 ns |      - |         - |
