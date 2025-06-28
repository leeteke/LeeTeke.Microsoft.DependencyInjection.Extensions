using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeeTeke.Microsoft.DependencyInjection.Extensions
{

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class DependencyRegisterAttribute : Attribute
    {
        internal Type Implementor { get;  }

        internal Type Service { get;  }
        internal Type[] Services { get; }
        internal DependencyRegisterType Type { get;  }

        /// <summary>
        /// 一对一模式
        /// </summary>
        /// <param name="service"></param>
        /// <param name="implementorType"></param>
        /// <param name="type"></param>
        public DependencyRegisterAttribute(Type service, Type implementorType, DependencyRegisterType @type = DependencyRegisterType.Singleton)
        {
            Services = null;
            Service = service;
            Implementor = implementorType;
            Type = @type;
        }
        /// <summary>
        /// 一对一模式
        /// </summary>
        /// <param name="implementorType"></param>
        /// <param name="type"></param>
        public DependencyRegisterAttribute(Type implementorType, DependencyRegisterType @type = DependencyRegisterType.Singleton)
        {
            Services = null;
            Service = Implementor = implementorType;
            Type = @type;
        }


        /// <summary>
        /// 一对多模式，注册类型为单例模式
        /// </summary>
        /// <param name="services"></param>
        /// <param name="implementorType"></param>
        public DependencyRegisterAttribute(Type[] services, Type implementorType, DependencyRegisterType @type = DependencyRegisterType.Singleton)
        {
            Service = null;
            Services = services;
            Implementor = implementorType;
            Type = @type;
        }

    }
}
