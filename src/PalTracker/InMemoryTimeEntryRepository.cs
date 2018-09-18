using System;
using System.Collections.Generic;
using System.Linq;

namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {

        private Dictionary<long, TimeEntry> _TimeEntries = new Dictionary<long, TimeEntry>();

        public bool Contains(long id)
        {
           //return _TimeEntries[id].Id != null;
           return _TimeEntries.Any(x => x.Key == id);
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            long timeEntryLenght = _TimeEntries.Count + 1;
            timeEntry.Id = timeEntryLenght;
            _TimeEntries.Add(timeEntryLenght, timeEntry);
            return _TimeEntries[timeEntryLenght];
        }

        public void Delete(long id)
        {
            _TimeEntries.Remove(id);
        }

        public TimeEntry Find(long id)
        {
            return _TimeEntries[id];
        }

        public IEnumerable<TimeEntry> List()
        {
            return _TimeEntries.Values.ToList();
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            timeEntry.Id = id;
            _TimeEntries[id] = timeEntry;
            return timeEntry;
        }
    }
}