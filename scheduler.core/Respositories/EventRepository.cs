using scheduler.core.Data;
using scheduler.core.Entities;
using scheduler.core.Interfaces;
using System.Linq;

namespace scheduler.core.Respositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _context;

        public EventRepository(DataContext context)
        {
            _context = context;
        }


        public object GetAll()
        {
            var result = _context.Events.ToList();

            return result;
        }

        public void Create(Event entity)
        {
            _context.Events.Add(entity);
            _context.SaveChanges();
        }
    }
}
