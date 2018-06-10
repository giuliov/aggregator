using Aggregator.Core.Interfaces;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aggregator.Core.Implementations
{
    public class WorkItemRepositoryExposed : IWorkItemRepositoryExposed
    {
        private WorkItemTrackingHttpClientBase witClient;
        private RequestContextBase context;

        public WorkItemRepositoryExposed(RequestContextBase context)
        {
            this.context = context;
            this.witClient = context.WitClient;
        }

        public IWorkItemExposed GetWorkItem(int workItemId)
        {
            var t = witClient.GetWorkItemAsync(workItemId, expand: WorkItemExpand.All, userState: context);
            t.Wait();
            if (t.IsCompletedSuccessfully)
            {
                return new WorkItemExposed(t.Result, witClient, context);
            }
            else
            {
                throw t.Exception;
            }
        }

        public IWorkItemExposed MakeNewWorkItem(IWorkItemExposed inSameProjectAs, string workItemTypeName)
        {
            string projectName = inSameProjectAs.Fields["System.TeamProject"].ToString();
            return MakeNewWorkItem(projectName, workItemTypeName);
        }

        public IWorkItemExposed MakeNewWorkItem(string projectName, string workItemTypeName)
        {
            var wi = new WorkItem
            {
                Id = -1,
                Fields = new Dictionary<string, object>()
            {
                { "System.TeamProject", projectName },
                { "System.WorkItemType", workItemTypeName}
            }
            };
            return new WorkItemExposed(wi, witClient, context);
        }

        public void AddItemToGlobalList(string globalListName, string item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetGlobalList(string globalListName)
        {
            throw new NotImplementedException();
        }

        public void RemoveItemFromGlobalList(string globalListName, string item)
        {
            throw new NotImplementedException();
        }
    }
}
