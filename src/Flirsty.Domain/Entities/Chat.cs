using Flirsty.Domain.Entities.Base;
using Flirsty.Domain.Entities.Enums;
using System;

namespace Flirsty.Domain.Entities
{
    public class Chat : EntityBase
    {
        public Guid InitiatorId { get; set; }

        public Guid RecipientId { get; set; }

        public ChatType Type { get; set; }

        public DateTime ExpireTime { get; set; }
    }
}