using Aggregator.Core.Interfaces;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Aggregator.Core.Implementations
{
    class FieldCollection : IFieldCollection
    {
        private WorkItemExposed workItemExposed;

        public FieldCollection(WorkItemExposed workItemExposed)
        {
            this.workItemExposed = workItemExposed;
        }

        public IField this[string name] {
            get => new Field(workItemExposed, name);
            set => workItemExposed.SetField(name, value);
        }

        public IEnumerator<IField> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
