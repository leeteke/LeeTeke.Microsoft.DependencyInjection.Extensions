using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LeeTeke.Microsoft.DependencyInjection.Extensions
{
    public static class ServiceCollectionServiceExtensions2
    {
        private static bool s_initialized;
        private static readonly object s_initializeLock = new object();

        public static IServiceCollection AddFromAssembliy(this IServiceCollection services)
        {
            return services.AddFromAssembliy(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IServiceCollection AddFromAssembliy(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (s_initialized)
            {
                return services;
            }

            lock (s_initializeLock)
            {
                if (s_initialized)
                {
                    return services;
                }



                for (int i = 0; i < assemblies.Length; i++)
                {

                    var customAttributes = assemblies[i].GetCustomAttributes(typeof(DependencyRegisterAttribute), true);
                    if (customAttributes != null)
                    {
                        foreach (DependencyRegisterAttribute attribute in customAttributes.Cast<DependencyRegisterAttribute>())
                        {
                            if (attribute.Service != null)
                            {
                                switch (attribute.Type)
                                {
                                    case DependencyRegisterType.Singleton:
                                        services.AddSingleton(attribute.Service, attribute.Implementor);
                                        break;
                                    case DependencyRegisterType.Transient:

                                        services.AddTransient(attribute.Service, attribute.Implementor);
                                        break;
                                    case DependencyRegisterType.Scoped:

                                        services.AddScoped(attribute.Service, attribute.Implementor);
                                        break;
                                    default:
                                        break;
                                }

                            }
                            else if (attribute.Services != null)
                            {
                                switch (attribute.Type)
                                {
                                    case DependencyRegisterType.Singleton:
                                        services.AddSingleton(attribute.Implementor);
                                        foreach (var service in attribute.Services)
                                        {
                                            //防重注册
                                            if (service != attribute.Implementor)
                                            {
                                                services.AddSingleton(service, p => p.GetService(attribute.Implementor));
                                            }
                                        }

                                        break;
                                    case DependencyRegisterType.Transient:
                                        services.AddTransient(attribute.Implementor);
                                        foreach (var service in attribute.Services)
                                        {
                                            //防重注册
                                            if (service != attribute.Implementor)
                                            {
                                                services.AddTransient(service, p => p.GetService(attribute.Implementor));
                                            }
                                        }

                                        break;
                                    case DependencyRegisterType.Scoped:
                                        services.AddScoped(attribute.Implementor);
                                        foreach (var service in attribute.Services)
                                        {
                                            //防重注册
                                            if (service != attribute.Implementor)
                                            {
                                                services.AddScoped(service, p => p.GetService(attribute.Implementor));
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }




                            }

                        }
                    }
                }

                s_initialized = true;
            }

            return services;
        }

        public static IServiceCollection AddFromNameSpace(this IServiceCollection services, string fullNameSpace, DependencyRegisterType dependencyRegisterType = DependencyRegisterType.Singleton)
        {

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                var impls = assemblies[i].ExportedTypes.Where(p => p.FullName.StartsWith(fullNameSpace) &&p.IsClass);
                if (impls != null)
                {
                    foreach (var impl in impls)
                    {
                        var ints = impl.GetInterfaces();
                        if (ints.Length < 1)
                        {

                        }
                        else if (ints.Length == 1)
                        {

                            switch (dependencyRegisterType)
                            {
                                case DependencyRegisterType.Singleton:
                                    services.AddSingleton(ints[0], impl);
                                    break;
                                case DependencyRegisterType.Transient:
                                    services.AddTransient(ints[0], impl);
                                    break;
                                case DependencyRegisterType.Scoped:
                                    services.AddScoped(ints[0], impl);
                                    break;
                                default:
                                    break;
                            }

                        }
                        else
                        {
                            switch (dependencyRegisterType)
                            {
                                case DependencyRegisterType.Singleton:
                                    services.AddSingleton(impl);
                                    foreach (var @int in ints)
                                    {
                                        services.AddSingleton(@int, p => p.GetService(impl));
                                    }

                                    break;
                                case DependencyRegisterType.Transient:
                                    services.AddTransient(impl);
                                    foreach (var @int in ints)
                                    {
                                        services.AddTransient(@int, p => p.GetService(impl));
                                    }
                                    break;
                                case DependencyRegisterType.Scoped:
                                    services.AddScoped(impl);
                                    foreach (var @int in ints)
                                    {
                                        services.AddScoped(@int, p => p.GetService(impl));
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                }
                    
            }

           

            return services;
        }
    }
}
