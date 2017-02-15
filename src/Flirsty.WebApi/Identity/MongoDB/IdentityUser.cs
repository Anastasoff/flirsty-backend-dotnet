using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Flirsty.WebApi.Identity.MongoDB
{
    public class IdentityUser : IUser<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual Guid DomainUserId { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual ICollection<IdentityUserClaim> Claims { get; } = new List<IdentityUserClaim>();

        public virtual ICollection<UserLoginInfo> Logins { get; } = new List<UserLoginInfo>();
    }
}