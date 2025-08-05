using Demo.Dll;
using LeeTeke.Microsoft.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:DependencyRegister(typeof(IDllTest),typeof(DllTestService), DependencyRegisterType.Scoped)]
namespace Demo.Dll
{
    public class DllTestService : IDllTest
    {
        public string Msg { get; }
        public DllTestService()
        {
            Msg = $"DllTestService:\tGUID:{Guid.NewGuid()}\t CreateTime:{DateTime.Now.ToFileTime()}";
        }
       
    }
}
