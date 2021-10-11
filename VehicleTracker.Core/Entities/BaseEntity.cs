using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Core.Exceptions;

namespace VehicleTracker.Core.Entities
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        private readonly List<object> _events;
        protected void Apply(object @event)
        {
            When(_events);
            _events.Add(@event);
        }
        public IEnumerable<object> GetChanges() => _events.AsEnumerable();
        public void ClearChanges() => _events.Clear();
        protected abstract void When(object @event);
        protected BaseEntity() { }
        public BaseEntity(T id)
        {
            if (default(T).Equals(id))
                throw new InvalidEntityIdException("Id is notpassed or is invalid");
            Id = id;
            _events = new List<object>();
        }
    }
}
