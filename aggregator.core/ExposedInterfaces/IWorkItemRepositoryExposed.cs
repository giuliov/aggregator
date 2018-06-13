using System.Collections.Generic;

namespace Aggregator.Core.Interfaces
{
    /// <summary>
    /// This interface is visible to scripts.
    /// </summary>
    public interface IWorkItemRepositoryExposed
    {
        IWorkItemExposed GetWorkItem(int workItemId);

        IWorkItemExposed MakeNewWorkItem(string projectName, string workItemTypeName);

        IWorkItemExposed MakeNewWorkItem(IWorkItemExposed inSameProjectAs, string workItemTypeName);

        IEnumerable<string> GetGlobalList(string globalListName);

        void AddItemToGlobalList(string globalListName, string item);

        void RemoveItemFromGlobalList(string globalListName, string item);
    }
}