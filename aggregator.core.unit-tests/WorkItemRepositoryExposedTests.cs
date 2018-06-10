using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Aggregator.Core.Implementations;
using Aggregator.Core;
using Aggregator.Core.UnitTests.Stubs;

namespace Aggregator.Core.UnitTests
{
    public class WorkItemRepositoryExposedTests
    {
        [Fact]
        void GetWorkItem_ById_Succeeds()
        {
            var context = new StubRequestContext();
            var sut = new WorkItemRepositoryExposed(context);

            var wi = sut.GetWorkItem(42);

            Assert.NotNull(wi);
            Assert.Equal(42, wi.Id);
        }

        [Fact]
        void MakeNewWorkItem_UsingProjectName_Succeeds()
        {
            var context = new StubRequestContext();
            var sut = new WorkItemRepositoryExposed(context);

            var wi = sut.MakeNewWorkItem("myproject", "mytype");

            Assert.NotNull(wi);
            Assert.Equal(-1, wi.Id);
            Assert.Equal("mytype", wi.TypeName);
        }

        [Fact]
        void MakeNewWorkItem_UsingSample_Succeeds()
        {
            var context = new StubRequestContext();
            var sut = new WorkItemRepositoryExposed(context);

            var sampleWi = sut.GetWorkItem(42);
            var wi = sut.MakeNewWorkItem(sampleWi, "mytype");

            Assert.NotNull(wi);
            Assert.Equal(-1, wi.Id);
            Assert.Equal("mytype", wi.TypeName);
        }
    }
}
