using ArchitecturalStudioTradition.Domain.Authors.Rules;
using ArchitecturalStudioTradition.Domain.SeedWork;

namespace ArchitecturalStudioTradition.Domain.Authors
{
    public class Author : Entity
    {
        public string Name { get; }
        public string Surname { get; }

        private Author(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public static Author Create(string name, string surname)
        {
            Validate(new NameMustBeSet(name));
            Validate(new SurnameMustBeSet(surname));

            return new Author(name, surname);
        }
    }
}
