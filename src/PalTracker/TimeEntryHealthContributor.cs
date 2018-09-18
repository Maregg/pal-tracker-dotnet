using System.Linq;
using Steeltoe.Common.HealthChecks;
using static Steeltoe.Common.HealthChecks.HealthStatus;

namespace PalTracker
{
    public class TimeEntryHealthContributor : IHealthContributor
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        public string Id {get;} = "timeEntry";

        public const int MaxTimeEntries = 5;

        public TimeEntryHealthContributor(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }
        public HealthCheckResult Health()
        {
           var count = _timeEntryRepository.List().Count();
           var status = count < MaxTimeEntries ? UP : DOWN;

           var health = new HealthCheckResult();
           health.Status = status;

           health.Details.Add("threshold", MaxTimeEntries);
           health.Details.Add("count", count);
           health.Details.Add("status", status.ToString());

           return health;           
        }
    }
}