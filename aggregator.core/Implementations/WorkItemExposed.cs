using Aggregator.Core.Interfaces;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aggregator.Core.Implementations
{
    public class WorkItemExposed : IWorkItemExposed
    {
        private WorkItem current;
        private readonly WorkItemTrackingHttpClientBase witClient;
        private RequestContextBase context;
        private FieldsChangeLog changeLog;

        public WorkItemExposed(WorkItem original, WorkItemTrackingHttpClientBase witClient, RequestContextBase context)
        {
            this.current = original ?? throw new ArgumentNullException(nameof(original));
            this.witClient = witClient ?? throw new ArgumentNullException(nameof(witClient));
            this.context = context;
            changeLog = new FieldsChangeLog()
            {
                WorkItemId = original.Id.GetValueOrDefault(0),
                RevisionNo = original.Rev.GetValueOrDefault(0)
            };
        }

        public object this[string name]
        {
            get => current.Fields[name];
            set => SetField(name, value);
        }

        internal void SetField(string name, object value)
        {
            changeLog.Add(name, value, current.Fields[name]);
            current.Fields[name] = value;
        }

        public IFieldCollection Fields => new FieldCollection(this);

        public string History
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public DateTime RevisedDate => throw new NotImplementedException();

        public int Revision => throw new NotImplementedException();

        public IRevision LastRevision => throw new NotImplementedException();

        public IRevision PreviousRevision => throw new NotImplementedException();

        public IRevision NextRevision => throw new NotImplementedException();

        public string TypeName => current.Fields["System.WorkItemType"].ToString();

        public int Id => current.Id.GetValueOrDefault(0);

        public Uri Uri => new Uri(current.Url);

        public IWorkItemExposed Parent
        {
            get
            {
                var rel = current.Relations.Where(r => r.Rel == "System.LinkTypes.Hierarchy-Reverse").FirstOrDefault();
                int parentId = ParseIdFromUrl(rel);
                var t = witClient.GetWorkItemAsync(parentId, expand: WorkItemExpand.All, userState: context);
                return new WorkItemExposed(t.Result, witClient, context);
            }
        }

        private static int ParseIdFromUrl(WorkItemRelation rel)
        {
            //HACK
            return int.Parse(rel.Url.Substring(1 + rel.Url.LastIndexOf('/')));
        }

        public IEnumerable<IWorkItemExposed> Children
        {
            get
            {
                foreach (var rel in current.Relations.Where(r => r.Rel == "System.LinkTypes.Hierarchy-Forward"))
                {
                    int childId = ParseIdFromUrl(rel);
                    var t = witClient.GetWorkItemAsync(childId, expand: WorkItemExpand.All, userState: context);
                    yield return new WorkItemExposed(t.Result, witClient, context);
                }
            }
        }

        public IWorkItemLinkExposedCollection WorkItemLinks => throw new NotImplementedException();

        public void AddHyperlink(string destination)
        {
            throw new NotImplementedException();
        }

        public void AddHyperlink(string destination, string message)
        {
            throw new NotImplementedException();
        }

        public void AddWorkItemLink(IWorkItemExposed destination, string linkTypeName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IWorkItemExposed> GetRelatives(IFluentQuery query)
        {
            throw new NotImplementedException();
        }

        public bool HasRelation(string relation)
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public void RemoveWorkItemLink(IWorkItemLinkExposed link)
        {
            throw new NotImplementedException();
        }

        public void TransitionToState(string state, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
