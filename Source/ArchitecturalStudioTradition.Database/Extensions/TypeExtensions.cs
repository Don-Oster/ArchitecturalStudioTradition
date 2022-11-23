using System.ComponentModel.DataAnnotations.Schema;

namespace ArchitecturalStudioTradition.Database.Extensions
{
    public static class TypeExtensions
    {
        public static string TableName(this Type type)
        {
            if (type == null)
                throw new ArgumentException($"Value for {nameof(type)} not provided", nameof(type));

            return type.GetCustomAttributes(typeof(TableAttribute), true)
                .OfType<TableAttribute>()
                .Single()
                .Name;
        }
    }
}
