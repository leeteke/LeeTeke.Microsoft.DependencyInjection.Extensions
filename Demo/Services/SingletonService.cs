using Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeeTeke.Microsoft.DependencyInjection.Extensions;
using Demo.Services;

//可以写在任何命名空间上，这里较为直观
[assembly:DependencyRegister(typeof(ISingletonTest),typeof(SingletonService))]
namespace Demo.Services
{
    internal class SingletonService : ISingletonTest
    {
        public string Msg { get; }

        public SingletonService()
        {
            Msg = $"SingletonTest:\tGUID:{Guid.NewGuid()}\t CreateTime:{DateTime.Now.ToFileTime()}";
        }
    }
}
