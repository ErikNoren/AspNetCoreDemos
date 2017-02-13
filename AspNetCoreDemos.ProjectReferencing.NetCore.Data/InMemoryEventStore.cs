using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreDemos.ProjectReferencing.NetCore.Data
{
    public class InMemoryEventStore : IEventStore
    {
        private List<Event> _events;

        public InMemoryEventStore()
        {
            _events = new List<Event>();
            SeedEventStore();
        }

        public IQueryable<Event> GetEvents()
        {
            return _events.AsQueryable();
        }

        public Event FindEvent(int id)
        {
            return _events.SingleOrDefault(e => e.Id == id);
        }

        public Event AddEvent(Event newEvent)
        {
            int maxId = _events.Max(e => e.Id);

            newEvent.Id = maxId + 1;
            _events.Add(newEvent);

            return newEvent;
        }

        public bool UpdateEvent(int id, Event modifiedEvent)
        {
            var found = FindEvent(id);

            if (found == null)
                return false;

            DeleteEvent(id);
            AddEvent(modifiedEvent);

            return true;
        }

        public bool DeleteEvent(int id)
        {
            var found = FindEvent(id);
            if (found != null)
            {
                return _events.Remove(found);
            }

            return false;
        }

        private void SeedEventStore()
        {
            _events.AddRange(new Event[] {
                new Event() { Id = 1, Name = "My First Event", Description = "The Hello World of events.", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now },
                new Event() { Id = 2, Name = "Another Event", Description = "Because you can never have just one event.", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now },
                new Event() { Id = 3, Name = "Third Seeded Event", Description = "Example text gets harder over time, uh, event.", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now },
                new Event() { Id = 4, Name = "One Seed To Go", Description = "Nearing the end of this seeding code event.", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now },
                new Event() { Id = 5, Name = "Final Seed Event", Description = "Final event from the seed operation event.", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now }
            });
        }
    }
}
