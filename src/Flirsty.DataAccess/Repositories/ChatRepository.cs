using Flirsty.DataAccess.Repositories.Base;
using Flirsty.Domain.Entities;
using Flirsty.Domain.Repositories;

namespace Flirsty.DataAccess.Repositories
{
    public class ChatRepository : MongoDbRepository<Chat>, IChatRepository
    {
    }
}