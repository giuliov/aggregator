using System;
using System.Collections.Generic;
using System.Text;

namespace Aggregator.Core.UnitTests.Stubs
{
    internal class StubRequestContext : RequestContextBase
    {
        public StubRequestContext()
        {
            this.WitClient = new StubWorkItemTrackingHttpClient(null, null);
        }
    }
}
