using System;

namespace Truphone.Domain.Entities
{
    public abstract record BaseEntity
    {
        public long Id { get; init; }
        public DateTime DateCreated { get; init; }
        public DateTime DateUpdated { get; set; }
    }
}
