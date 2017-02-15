using Flirsty.DataAccess.Repositories.Base;
using Flirsty.Domain.Entities;
using Flirsty.Domain.Repositories;

namespace Flirsty.DataAccess.Repositories
{
    public class InterestRepository : MongoDbRepository<Interest>, IInterestRepository
    {
    }
}