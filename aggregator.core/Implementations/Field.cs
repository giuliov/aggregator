using System;
using Aggregator.Core.Interfaces;

namespace Aggregator.Core.Implementations
{
    internal class Field : IField
    {
        private WorkItemExposed workItemExposed;
        private string name;

        public Field(WorkItemExposed workItemExposed, string name)
        {
            this.workItemExposed = workItemExposed;
            this.name = name;
        }

        public string Name => name;

        public string ReferenceName => name;

        public object Value {
            get => workItemExposed.Fields[name];
            set => workItemExposed.SetField(name,value);
        }

        // MISSING!
        public Type DataType => throw new NotImplementedException();
    }
}