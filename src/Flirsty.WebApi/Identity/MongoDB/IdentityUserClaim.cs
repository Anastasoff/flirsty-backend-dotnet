namespace Flirsty.WebApi.Identity.MongoDB
{
    public class IdentityUserClaim
    {
        public virtual int Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }
    }
}