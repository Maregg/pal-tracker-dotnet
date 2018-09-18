using Steeltoe.Management.Endpoint.Info;

namespace PalTracker

{
    public class TimeEntryInfoContributor : IInfoContributor
    {


        public TimeEntryInfoContributor(IOperationCounter<TimeEntry> operationCounter)
        {
          _operationCounter = operationCounter;    
        }

        public IOperationCounter<TimeEntry> _operationCounter { get; }

        public void Contribute(IInfoBuilder builder)
        {
            builder.WithInfo(
                _operationCounter.Name,
                _operationCounter.GetCounts
            );
        }
    }
}