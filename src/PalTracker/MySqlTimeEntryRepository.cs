using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    

    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        public TimeEntryContext _context { get; private set; }

        public MySqlTimeEntryRepository(TimeEntryContext context)
        {
            _context = context;
            
        }

        public bool Contains(long id)
        {
           return _context.TimeEntryRecords.AsNoTracking().Any(x => x.Id == id);
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var recordToCreate = timeEntry.ToRecord();

            _context.TimeEntryRecords.Add(recordToCreate);
            _context.SaveChanges();

            return Find(recordToCreate.Id.Value);
        }

        public void Delete(long id)
        {
            _context.TimeEntryRecords.Remove(FindRecord(id));
            _context.SaveChanges();
        }

        public TimeEntry Find(long id)
        {
            return FindRecord(id).ToEntity();
        }

        public IEnumerable<TimeEntry> List()
        {
            return _context.TimeEntryRecords.AsNoTracking().Select(x => x.ToEntity());
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            var timeEntryMapped = timeEntry.ToRecord();
            timeEntryMapped.Id = id;
            _context.Update(timeEntryMapped);
            _context.SaveChanges();

            return Find(id);
        }

        private TimeEntryRecord FindRecord(long id) =>
            _context.TimeEntryRecords.AsNoTracking().Single(t => t.Id == id);
    }

}