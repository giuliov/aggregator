using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aggregator.Core
{
    public class RequestContextBase
    {
        public WorkItemTrackingHttpClientBase WitClient { get; protected set; }
        public string ProjectName { get; protected set; }
    }
}
