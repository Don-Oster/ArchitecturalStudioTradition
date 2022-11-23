using ArchitecturalStudioTradition.Domain.Authors;
using ArchitecturalStudioTradition.Domain.Photos;
using ArchitecturalStudioTradition.Domain.Projects.Events;
using ArchitecturalStudioTradition.Domain.SeedWork;

namespace ArchitecturalStudioTradition.Domain.Projects
{
    public class Project : Entity, IAggregateRoot
    {
        public string Name { get; }
        public Author TextAuthor { get;}
        public Author PhotoAuthor { get; }
        public IReadOnlyCollection<Author> ProjectAuthors { get; }
        public DateTime PublicationDate { get; }
        public IReadOnlyCollection<Photo> Photos { get; }

        private Project(string name, Author textAuthor, Author photoAuthor, IReadOnlyCollection<Author> projectAuthors, IReadOnlyCollection<Photo> photos)
        {
            Name = name;
            TextAuthor = textAuthor;
            PhotoAuthor = photoAuthor;
            ProjectAuthors = projectAuthors;
            PublicationDate = Clock.Now;
            Photos = photos;

            AddDomainEvent(new ProjectCreated(Name));
        }

        public static Project Create(string name, Author textAuthor, Author photoAuthor, IReadOnlyCollection<Author> projectAuthors, IReadOnlyCollection<Photo> photos)
        {
            return new Project(name, textAuthor, photoAuthor, projectAuthors, photos);
        }
    }
}
