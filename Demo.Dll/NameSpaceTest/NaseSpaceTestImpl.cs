using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dll.NameSpaceTest
{
    public class NaseSpaceTestImpl : INameSpaceTest, INameSpaceTest2
    {
        public string Msg { get; }

        public string Msg2 { get; }

        public NaseSpaceTestImpl()
        {
            Msg2 = Msg = $"DllTestService:\tGUID:{Guid.NewGuid()}\t CreateTime:{DateTime.Now.ToFileTime()}";
        }
    }
}
