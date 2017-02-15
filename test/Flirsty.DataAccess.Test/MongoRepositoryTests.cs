using Flirsty.DataAccess.Repositories.Base;
using Flirsty.DataAccess.Test.Helpers;
using Flirsty.Domain.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flirsty.Domain.Entities.ValueObjects;
using Xunit;

namespace Flirsty.DataAccess.Test
{
    public class EntityRootFake : EntityBase
    {
        public EntityRootFake()
        {
            EntityLeafFakes = new HashSet<EntityLeafFake>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EntityLeafFake> EntityLeafFakes { get; set; }
    }

    public class EntityLeafFake : EntityBase
    {
        public EntityLeafFake()
        {
            EntityLeafFakes = new HashSet<EntityLeafFake>();
        }

        public string Name { get; set; }

        public ICollection<EntityLeafFake> EntityLeafFakes { get; set; }
    }

    public class MongoDbRepositoryMock : MongoDbRepository<EntityRootFake>
    {
    }

    public class MongoRepositoryTests
    {
        [Fact]
        public async Task Add_Should_Insert_New_Mocked_Entity()
        {
            var repo = new MongoDbRepositoryMock();

            var entity = new EntityRootFake
            {
                FirstName = "Pesho_" + StringGenerator.GenerateTimeStamp(),
                LastName = "Peshev"
            };

            var entityLeaf = new EntityLeafFake
            {
                Name = "FakeTaxi"
            };

            entityLeaf.EntityLeafFakes.Add(new EntityLeafFake { Name = "FakeTaxi" });

            entity.EntityLeafFakes.Add(entityLeaf);

            await repo.AddAsync(entity);
        }
    }
}