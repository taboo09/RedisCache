using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using DisplayNames.Models;

namespace DisplayNames.FakeData
{
    public static class DataPersons
    {
        public static async Task<IEnumerable<PersonModel>> Generate(int count)
        {
            var fakePersons = new List<PersonModel>();

            for (int i = 0; i < count; i++)
            {
                fakePersons.Add
                (
                    new Faker<PersonModel>()
                        .StrictMode(true)
                        .RuleFor(x => x.PersonId, f => f.Random.Guid())
                        .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                        .RuleFor(x => x.LastName, f => f.Person.LastName)
                        .RuleFor(x => x.City, f => f.Person.Address.City)
                        .RuleFor(x => x.Email, f => f.Person.Email)
                        .RuleFor(x => x.Website, f => f.Person.Website)
                        .Generate()
                );
            }

            return (await Task.FromResult(fakePersons));
        }
    }
}