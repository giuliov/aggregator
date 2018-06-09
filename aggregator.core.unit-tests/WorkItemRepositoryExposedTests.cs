using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Aggregator.Core.Implementations;
using Aggregator.Core;

namespace Aggregator.Core.UnitTests
{
    public class WorkItemRepositoryExposedTests
    {
        [Fact]
        void GetWorkItem_ById_Succeeds()
        {
            var witClient = new StubWorkItemTrackingHttpClient(null, null);
            var sut = new WorkItemRepositoryExposed(witClient);

            var wi = sut.GetWorkItem(42);

            Assert.NotNull(wi);
            Assert.Equal(42, wi.Id);
        }

        [Fact]
        void MakeNewWorkItem_UsingProjectName_Succeeds()
        {
            var witClient = new StubWorkItemTrackingHttpClient(null, null);
            var sut = new WorkItemRepositoryExposed(witClient);

            var wi = sut.MakeNewWorkItem("myproject", "mytype");

            Assert.NotNull(wi);
            Assert.Equal(0, wi.Id);
            Assert.Equal("mytype", wi.TypeName);
        }

        [Fact]
        void MakeNewWorkItem_UsingSample_Succeeds()
        {
            var witClient = new StubWorkItemTrackingHttpClient(null, null);
            var sut = new WorkItemRepositoryExposed(witClient);

            var sampleWi = sut.GetWorkItem(42);
            var wi = sut.MakeNewWorkItem(sampleWi, "mytype");

            Assert.NotNull(wi);
            Assert.Equal(0, wi.Id);
            Assert.Equal("mytype", wi.TypeName);
        }

        [Fact]
        void GetWorkItem_ThenParent_Succeeds()
        {
            var witClient = new StubWorkItemTrackingHttpClient(null, null);
            var sut = new WorkItemRepositoryExposed(witClient);

            var wi = sut.GetWorkItem(42);
            var parentWi = wi.Parent;

            Assert.NotNull(parentWi);
            Assert.Equal(33, parentWi.Id);
        }
    }
}
