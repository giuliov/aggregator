﻿using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregator.Core
{
    internal class StubWorkItemTrackingHttpClient : WorkItemTrackingHttpClientBase
    {
        public StubWorkItemTrackingHttpClient(Uri baseUrl, VssCredentials credentials)
            : base(baseUrl, credentials)
        {
        }

        public override Task<WorkItem> GetWorkItemAsync(int id, IEnumerable<string> fields = null, DateTime? asOf = null, WorkItemExpand? expand = null, object userState = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var t = new Task<WorkItem>(() => new WorkItem()
            {
                Id = id,
                Fields = new Dictionary<string, object>()
                {
                    { "System.WorkItemType", "Bug" },
                    { "System.State", "Open" },
                    { "System.TeamProject", "MyProject" }
                },
                Rev = 99
            });
            t.RunSynchronously();
            return t;
        }

        public override Task<WorkItemType> GetWorkItemTypeAsync(Guid project, string type, object userState = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var t = new Task<WorkItemType>(() => new WorkItemType()
            {
                Name = "Bug",
                IsDisabled = false,
                FieldInstances = new List<WorkItemTypeFieldInstance>()
                {
                    new WorkItemTypeFieldInstance()
                    {
                        ReferenceName = "System.State",
                        IsIdentity = false,
                        AlwaysRequired = true
                        // MISSING: Data type!
                    }
                },
                Transitions = new Dictionary<string, WorkItemStateTransition[]>()
                {
                    {
                        "New", (new WorkItemStateTransition[] {
                            new WorkItemStateTransition()
                            {
                                To = "Active", Actions = new string[] { "" }
                            }
                        })
                    },
                    {
                        "Active", (new WorkItemStateTransition[] {
                            new WorkItemStateTransition()
                            {
                                To = "Closed", Actions = new string[] { "" }
                            }
                        })
                    }
                }
            });
            t.RunSynchronously();
            return t;
        }

        public override Task<WorkItemQueryResult> QueryByWiqlAsync(Wiql wiql, Guid project, bool? timePrecision = null, int? top = null, object userState = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var t = new Task<WorkItemQueryResult>(() => new WorkItemQueryResult() {
                QueryType = QueryType.Flat,
                WorkItems = new List<WorkItemReference>()
                {
                    new WorkItemReference()
                    {
                        Id = 33,
                        Url = $"https://example.visualstudio.com/{project}/_apis/wit/workItems/33"
                    }
                }
            });

            t.RunSynchronously();
            return t;
        }
    }
}
