namespace Tkd.Simsa.Persistence.Mapper;

using System.Linq.Expressions;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.PersonManagement;
using Tkd.Simsa.Persistence.Entities;

internal class PersonMapper : IMapper<PersonEntity, Person>
{
    public IPropertyMapper<PersonEntity, Person> PropertyMapper { get; } = new PersonPropertyMapper();

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

    private class PersonPropertyMapper : PropertyMapperBase<PersonEntity, Person>
    {
        protected override Dictionary<string, Expression<Func<PersonEntity, object>>> PropertyMap { get; } = new ()
        {
            { nameof(Person.DateOfBirth), i => i.DateOfBirth },
            { nameof(Person.Name.FirstName), i => i.FirstName },
            { nameof(Person.Name.LastName), i => i.LastName },
            { nameof(Person.Gender), i => i.Gender }
        };
    }
}