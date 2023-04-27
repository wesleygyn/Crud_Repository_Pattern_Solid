using System;

namespace Core.Models
{
    public class Entity
    {
        public Guid Id { get; set; }

        protected Entity (Guid id)
        {
            Id = Guid.NewGuid();
        }
    }
}