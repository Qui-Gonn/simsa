namespace Simsa.Persistence.Repositories;

using Simsa.Core.Repositories;
using Simsa.Model;
using Simsa.Persistence.Entities;
using Simsa.Persistence.Mapper;

internal class PersonRepository : GenericRepository<PersonEntity, Person>, IPersonRepository
{
    public PersonRepository(SimsaDbContext dbContext, IMapper<PersonEntity, Person> mapper)
        : base(dbContext, mapper)
    {
    }
}