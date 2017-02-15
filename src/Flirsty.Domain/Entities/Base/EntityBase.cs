using System;

namespace Flirsty.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        public override int GetHashCode() => Id.GetHashCode() ^ 317;
    }
}