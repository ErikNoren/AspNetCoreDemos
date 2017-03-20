using System.Linq;

namespace AspNetCoreDemos.ProjectReferencing.Data
{
    public interface IEventStore
    {
        Event AddEvent(Event newEvent);
        bool DeleteEvent(int id);
        Event FindEvent(int id);
        IQueryable<Event> GetEvents();
        bool UpdateEvent(int id, Event modifiedEvent);
    }
}