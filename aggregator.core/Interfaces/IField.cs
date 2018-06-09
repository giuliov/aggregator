using Aggregator.Core.Interfaces;
using System;

namespace Aggregator.Core.Interfaces
{
    /// <summary>
    /// Decouples Core from TFS Client API <see cref="Microsoft.TeamFoundation.WorkItemTracking.Client.Field"/>
    /// </summary>
    public interface IField
    {
        string Name { get; }

        string ReferenceName { get; }

        object Value { get; set; }

        // BREAKING CHANGE
        //FieldStatus Status { get; }

        // BREAKING CHANGE
        //object OriginalValue { get; }

        Type DataType { get; }
    }
}
