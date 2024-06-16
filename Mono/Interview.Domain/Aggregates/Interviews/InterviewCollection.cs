using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.Interviews
{
    public class InterviewCollection : EntityBase<Guid>
    {
        public InterviewCollection() { }
        public InterviewCollection(string collectionName, bool isApplied)
        {
            CollectionName = collectionName;
            IsApplied = isApplied;
            CreatedAt = DateTime.Now;
        }
        
        public string CollectionName { get; set; }
        public bool IsApplied { get; set; }
        private List<InterviewProcess> processes { get; set; } = new();
        public IReadOnlyCollection<InterviewProcess> Processes => processes;

        public void ResetApplyStatus()
        {
            IsApplied = false;
        }

        public void AddStep(InterviewProcess process)
        {
            processes.Add(process);
        }
    }
}
