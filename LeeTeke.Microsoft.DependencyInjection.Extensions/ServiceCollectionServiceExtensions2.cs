using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LeeTeke.Microsoft.DependencyInjection.Extensions
{
    public static class ServiceCollectionServiceExtensions2
    {
        private static bool s_initialized;
        private static readonly object s_initializeLock = new object();
        public static IServiceCollection AddFromAssembliy(this IServiceCollection services)
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

                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                for (int i = 0; i < assemblies.Length; i++)
                {



                    var customAttributes = assemblies[i].GetCustomAttributes(typeof(DependencyRegisterAttribute), true);
                    if (customAttributes == null)
                    {
                        continue;
                    }

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
                                        services.AddSingleton(service, p => p.GetService(attribute.Implementor));
                                    }

                                    break;
                                case DependencyRegisterType.Transient:
                                    services.AddTransient(attribute.Implementor);
                                    foreach (var service in attribute.Services)
                                    {
                                        services.AddTransient(service, p => p.GetService(attribute.Implementor));
                                    }
                                   
                                    break;
                                case DependencyRegisterType.Scoped:
                                    services.AddScoped(attribute.Implementor);
                                    foreach (var service in attribute.Services)
                                    {
                                        services.AddScoped(service, p => p.GetService(attribute.Implementor));
                                    }
                                    break;
                                default:
                                    break;
                            }


                          

                        }

                    }

                }

                s_initialized = true;
            }

            return services;
        }
    }
}
