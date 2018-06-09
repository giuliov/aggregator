using System;
using System.Collections.Generic;
using System.Text;

namespace Aggregator.Core
{
    internal class FieldsChangeLog
    {
        public int WorkItemId;
        public int RevisionNo;
        private int lastSequenceNo = 1;
        List<FieldChangeRecord> Log = new List<FieldChangeRecord>();

        public void Add(string referenceName, object newValue,object oldValue)
        {
            this.Log.Add(new FieldChangeRecord()
            {
                NewValue = newValue,
                OldValue = oldValue,
                ReferenceName = referenceName,
                Sequence = lastSequenceNo++
            });
        }
    }

    internal class FieldChangeRecord
    {
        public int Sequence;
        public string ReferenceName;
        public object OldValue;
        public object NewValue;
    }
}
