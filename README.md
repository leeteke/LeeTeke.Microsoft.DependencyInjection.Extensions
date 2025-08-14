# LeeTeke.Microsoft.DependencyInjection.Extensions
本项目为Microsoft.DependencyInjection 的扩展方法使用，主要用于注册。  

   ***可配合 LeeTeke.Microsoft.DependencyInjection.Extensions.AOT使用***

 ## Nuget
[![NUGET](https://img.shields.io/badge/nuget-1.2.2-blue.svg)](https://www.nuget.org/packages/LeeTeke.Microsoft.DependencyInjection.Extensions)

    dotnet add package LeeTeke.Microsoft.DependencyInjection.Extensions

主要使用如下：
```csharp
//IOC生成器操作
using  LeeTeke.Microsoft.DependencyInjection.Extensions;
var services = new ServiceCollection();

//默认全部
services.AddFromAssembliy();
//指定程序集
service.AddFromAssembliy(typeof(Program).Assembliy);
//指定命名空间，该方法可不用DependencyRegisterAttribute来描述，适用于DDD领域层领域服务的注册。
//会扫描该NameSpace下的所有Class,并检查其接口,按照DependencyRegisterType的选项去自动注册。
services.AddFromNameSpaceImpl("Demo.Dll.NameSpaceTest", DependencyRegisterType.Scoped);

var ioc = services.BuildServiceProvider();
```

```csharp
//在命名空间处配置
//配置1
using  LeeTeke.Microsoft.DependencyInjection.Extensions;
[assembly:DependencyRegister(service: typeof(IService), implementorType: typeof(Service), @type:DependencyRegisterType.Singleton)]
namespace Demo.Services
{
    class Service : IService
    {
     .....
    }
}

//配置2
using  LeeTeke.Microsoft.DependencyInjection.Extensions;
[assembly:DependencyRegister(implementorType: typeof(Service), @type:DependencyRegisterType.Singleton)]
namespace Demo.Services
{
    class Service
    {
     .....
    }
}

//配置3
using  LeeTeke.Microsoft.DependencyInjection.Extensions;
[assembly:DependencyRegister(services: [typeof(IServiceA),typeof(IServiceB)], implementorType: typeof(Service), @type:DependencyRegisterType.Singleton)]
namespace Demo.Services
{
    class Service:IServiceA,IServiceB
    {
     .....
    }
}

```