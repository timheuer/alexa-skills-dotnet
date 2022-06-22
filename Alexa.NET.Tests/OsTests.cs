using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alexa.NET.Tests
{
    public class OsTests
    {
        [Fact]
        public void Ensure_Runs_On_Windows()
        {
            Assert.True(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));
        }

        [Fact]
        public void Ensure_Runs_On_Linux()
        {
            Assert.True(RuntimeInformation.IsOSPlatform(OSPlatform.Linux));
        }
    }
}
