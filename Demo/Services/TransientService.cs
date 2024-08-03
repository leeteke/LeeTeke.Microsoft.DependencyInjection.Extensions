using Demo.Interfaces;
using Demo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeeTeke.Microsoft.DependencyInjection.Extensions;
//可以写在任何命名空间上，这里较为直观
[assembly: DependencyRegister(typeof(ITransientTest), typeof(TransientService), DependencyRegisterType.Transient)]
namespace Demo.Services
{
    internal class TransientService : ITransientTest
    {
        public string Msg { get; }
        public TransientService()
        {
            Msg = $"TransientTest:\tGUID:{Guid.NewGuid()}\t CreateTime:{DateTime.Now.ToFileTime()}";
        }
    }
}
