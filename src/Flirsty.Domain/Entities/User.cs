using Flirsty.Domain.Entities.Base;
using Flirsty.Domain.Entities.Enums;
using Flirsty.Utilities;
using System;
using System.Collections.Generic;
using Flirsty.Domain.Entities.ValueObjects;

namespace Flirsty.Domain.Entities
{
    public class User : EntityBase
    {
        public User()
        {
            CreatedOn = DateTime.Now;
            ExternalKeys = new HashSet<ExternalKey>();
            FriendRequests = new HashSet<FriendRequest>();
            InterestsIds = new HashSet<Guid>();
            FriendsIds = new HashSet<Guid>();
        }

        public string Email { get; set; }

        public int Seconds { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public Gender? LookingFor { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public PublicInfo PublicInfo { get; set; }

        public Location Location { get; set; }

        public ICollection<ExternalKey> ExternalKeys { get; set; }

        public ICollection<FriendRequest> FriendRequests { get; set; }

        public ICollection<Guid> InterestsIds { get; set; }

        public ICollection<Guid> FriendsIds { get; set; }

        public int GetAge() => AgeCalculator.Calculate(DateTime.Today, BirthDate);
    }
}