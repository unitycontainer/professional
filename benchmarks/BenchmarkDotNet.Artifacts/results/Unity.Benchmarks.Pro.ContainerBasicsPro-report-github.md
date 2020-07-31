``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
AMD Ryzen Threadripper 2970WX, 1 CPU, 48 logical and 24 physical cores
.NET Core SDK=5.0.100-preview.8.20363.2
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.36102, CoreFX 5.0.20.36102), X64 RyuJIT
  Job-PAGBMN : .NET Core 5.0.0 (CoreCLR 5.0.20.36102, CoreFX 5.0.20.36102), X64 RyuJIT

Runtime=.NET Core 5.0  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                        Method |      Mean |    Error |    StdDev |
|------------------------------ |----------:|---------:|----------:|
|  Container.IsRegistered(true) | 10.616 ns | 3.911 ns | 0.2144 ns |
| Container.IsRegistered(false) |  6.721 ns | 2.397 ns | 0.1314 ns |
