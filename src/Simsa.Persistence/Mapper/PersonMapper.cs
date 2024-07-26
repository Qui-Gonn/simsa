namespace Simsa.Persistence.Mapper;

using Simsa.Model;
using Simsa.Persistence.Entities;

internal class PersonMapper : IMapper<PersonEntity, Person>
{
    public PersonEntity ToEntity(Person model)
        => this.UpdateEntity(new PersonEntity { Id = model.Id }, model);

    public Person ToModel(PersonEntity entity)
        => new ()
        {
            Id = entity.Id,
            DateOfBirth = BirthDate.FromDateOnly(entity.DateOfBirth),
            Gender = entity.Gender,
            Name = new PersonName(entity.FirstName, entity.LastName)
        };

    public PersonEntity UpdateEntity(PersonEntity entity, Person model)
    {
        entity.DateOfBirth = model.DateOfBirth.Date;
        entity.FirstName = model.Name.FirstName;
        entity.LastName = model.Name.LastName;
        entity.Gender = model.Gender;
        return entity;
    }
}