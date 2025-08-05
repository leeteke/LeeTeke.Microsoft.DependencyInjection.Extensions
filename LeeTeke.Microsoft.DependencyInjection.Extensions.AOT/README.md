# LeeTeke.Microsoft.DependencyInjection.Extensions
本项目为Microsoft.DependencyInjection 的扩展方法使用，主要用于注册，需要搭配LeeTeke.Microsoft.DependencyInjection.Extensions使用

   ***支持 native AOT***

 ## Nuget
[![NUGET](https://img.shields.io/badge/nuget-1.0.0-blue.svg)](https://www.nuget.org/packages/LeeTeke.Microsoft.DependencyInjection.Extensions)

    dotnet add package LeeTeke.Microsoft.DependencyInjection.Extensions
    dotnet add package LeeTeke.Microsoft.DependencyInjection.Extensions.AOT

主要使用如下：
```csharp
//AOT模式注册
using  LeeTeke.Microsoft.DependencyInjection.Extensions.AOT;
var services = new ServiceCollection();
services.AddFromAssembliyAOT();
var ioc = services.BuildServiceProvider();
```


