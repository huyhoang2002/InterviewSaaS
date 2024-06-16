using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.Interviews
{
    public class InterviewProcess : EntityBase<Guid>
    {
        public InterviewProcess() { }
        public InterviewProcess(string stepKey, string step, Guid interviewCollectionId)
        {
            StepKey = $"component_{stepKey}";
            Step = step;
            InterviewCollectionId = interviewCollectionId;
            CreatedAt = DateTime.Now;
        }

        public string StepKey { get; set; }
        public string Step { get; set; }         
        public Guid InterviewCollectionId { get; set; }
        public InterviewCollection InterviewCollection  { get; set; }
    }
}
