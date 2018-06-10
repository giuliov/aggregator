using Aggregator.Core.Implementations;
using Aggregator.Core.UnitTests.Stubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Aggregator.Core.UnitTests
{
    public class WorkItemExposedTests
    {
        WorkItemRepositoryExposed repository;
        public WorkItemExposedTests()
        {
            var context = new StubRequestContext();
            repository = new WorkItemRepositoryExposed(context);
        }

        [Fact]
        void GetParent_Succeeds()
        {
            var wi = repository.GetWorkItem(42);

            var parentWi = wi.Parent;

            Assert.NotNull(parentWi);
            Assert.Equal(33, parentWi.Id);
        }

        [Fact]
        void GetChildren_ReturnsTwo()
        {
            var wi = repository.GetWorkItem(42);

            var children = wi.Children;

            int numChildren = children.Count();
            Assert.Equal(2, numChildren);
        }
    }
}
