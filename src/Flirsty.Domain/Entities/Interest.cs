using Flirsty.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Flirsty.Domain.Entities
{
    public class Interest : EntityBase
    {
        public Interest()
        {
            InterestedUsersIds = new HashSet<Guid>();
        }

        public string Name { get; set; }

        public ICollection<Guid> InterestedUsersIds { get; set; }
    }
}