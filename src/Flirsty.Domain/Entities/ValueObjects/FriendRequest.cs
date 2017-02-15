using System;
using Flirsty.Domain.Entities.Enums;

namespace Flirsty.Domain.Entities.ValueObjects
{
    public class FriendRequest
    {
        public Guid OfferingId { get; set; }

        public Guid OffererId { get; set; }

        public FriendRequestStatus Status { get; set; }
    }
}