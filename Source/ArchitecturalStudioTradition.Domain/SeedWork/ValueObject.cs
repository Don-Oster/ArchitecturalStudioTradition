using ArchitecturalStudioTradition.Domain.SeedWork.Rules;
using System.Reflection;

namespace ArchitecturalStudioTradition.Domain.SeedWork
{
    [Serializable]
    public abstract class ValueObject : IComparable, IComparable<ValueObject>, IEquatable<ValueObject>
    {
        private List<FieldInfo> _fields;
        private List<PropertyInfo> _properties;
        private int? _cachedHashCode;

        protected static void Validate(IBusinessRule rule)
        {
            if (!rule.IsValid())
                throw new BusinessRuleValidationException(rule);
        }

        public bool Equals(ValueObject other)
        {
            return Equals(other as object);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            return GetProperties().All(p => Equals(p.GetValue(this, null), p.GetValue(obj, null))) 
                && GetFields().All(f => Equals(f.GetValue(this), f.GetValue(obj)));
        }

        public override int GetHashCode()
        {
            if (!_cachedHashCode.HasValue)
            {
                _cachedHashCode = GetEqualityComponents()
                    .Aggregate(1, (current, x) =>
                    {
                        unchecked
                        {
                            return current * 23 + (x?.GetHashCode() ?? 0);
                        }
                    });
            }

            return _cachedHashCode.Value;
        }

        public virtual int CompareTo(object obj)
        {
            if (GetType() != obj.GetType())
                return string.Compare(GetType().ToString(), obj.GetType().ToString(), StringComparison.Ordinal);

            var other = (ValueObject)obj;

            object[] components = GetEqualityComponents().ToArray();
            object[] otherComponents = other.GetEqualityComponents().ToArray();

            for (int i = 0; i < components.Length; i++)
            {
                int comparison = CompareComponents(components[i], otherComponents[i]);
                if (comparison != 0)
                    return comparison;
            }

            return 0;
        }

        public virtual int CompareTo(ValueObject other)
        {
            return CompareTo(other as object);
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        private int CompareComponents(object object1, object object2)
        {
            if (object1 is null && object2 is null)
                return 0;

            if (object1 is null)
                return -1;

            if (object2 is null)
                return 1;

            if (object1 is IComparable comparable1 && object2 is IComparable comparable2)
                return comparable1.CompareTo(comparable2);

            return object1.Equals(object2) ? 0 : -1;
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            return _properties ??= GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToList();
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            return _fields ??= GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .ToList();
        }

        private IEnumerable<object> GetEqualityComponents()
        {
            return GetProperties().Select(x => x.GetValue(this, null))
                    .Union(GetFields().Select(x => x.GetValue(this)))
                    .Where(x => x != null);
        }
    }
}