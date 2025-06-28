using Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeeTeke.Microsoft.DependencyInjection.Extensions;

using Demo.Services;
//可以写在任何命名空间上，这里较为直观
[assembly: DependencyRegister([typeof(IMultipleTestA), typeof(IMultipleTestB)], typeof(MultipleService), DependencyRegisterType.Singleton)]
namespace Demo.Services
{
    internal class MultipleService : IMultipleTestA, IMultipleTestB
    {
        public string Msg_A { get; }

        public string Msg_B { get; }


        public MultipleService()
        {
            Msg_A = Msg_B = $"MultipleTest:\tGUID:{Guid.NewGuid()}\t CreateTime:{DateTime.Now.ToFileTime()}";
        }
    }
}
