using Aggregator.Core.Interfaces;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aggregator.Core.Implementations
{
    public class WorkItemRepositoryExposed : IWorkItemRepositoryExposed
    {
        private WorkItemTrackingHttpClientBase witClient;

        public WorkItemRepositoryExposed(WorkItemTrackingHttpClientBase witClient)
        {
            this.witClient = witClient;
        }

        public IWorkItemExposed GetWorkItem(int workItemId)
        {
            var t = witClient.GetWorkItemAsync(workItemId);
            t.Wait();
            if (t.IsCompletedSuccessfully)
            {
                return new WorkItemExposed(t.Result, witClient);
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
            var wi = new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem();
            wi.Fields = new Dictionary<string, object>()
            {
                { "System.TeamProject", projectName },
                { "System.WorkItemType", workItemTypeName}
            };
            return new WorkItemExposed(wi, witClient);
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
