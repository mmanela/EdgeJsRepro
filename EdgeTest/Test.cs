using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EdgeTest
{
    public class Test
    {
        [Fact]
        public void Litmus()
        {
            Program.Run();

            Assert.True(true);
        }
    }
}
