using Microsoft.EntityFrameworkCore;
using scheduler.core.Data;
using scheduler.core.Entities;
using scheduler.core.Enums;
using scheduler.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scheduler.core.Respositories
{
    public sealed class EventRepository : IEventRepository
    {
        private readonly DataContext _context;

        public EventRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Event>> GetAllAsync()
        {
            var result = await _context.Events.ToListAsync();

            return result;
        }

        public async Task CreateAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task<Event> GetByIdAsync(Guid eventId)
        {
            var result = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            return result;
        }

        public void Delete(Event entity)
        {
            _context.Events.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<bool> ChangeStatusAsync(Guid id)
        {
            var entity = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (entity != null)
            {

                entity.EventStatusId = (int)EventStatusEnum.Cancelled;

                _context.Update(entity);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
