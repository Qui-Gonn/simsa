namespace Simsa.Persistence.Mapper;

using Simsa.Model;
using Simsa.Persistence.Entities;

internal class PersonMapper : IMapper<PersonEntity, Person>
{
    public PersonEntity ToEntity(Person model)
        => this.UpdateEntity(new PersonEntity { Id = model.Id }, model);

    public Person ToModel(PersonEntity entity)
        => new()
        {
            Id = entity.Id,
            DateOfBirth = entity.DateOfBirth,
            FirstName = entity.FirstName,
            Gender = entity.Gender,
            LastName = entity.LastName
        };

    public PersonEntity UpdateEntity(PersonEntity entity, Person model)
    {
        entity.DateOfBirth = model.DateOfBirth;
        entity.FirstName = model.FirstName;
        entity.LastName = model.LastName;
        entity.Gender = model.Gender;
        return entity;
    }
}