using System.ComponentModel;

namespace ArchitecturalStudioTradition.Common.Extensions
{
    public static class EnumExtensions
    {
        public static TAtributeType GetAttribute<TEnumType, TAtributeType>(this TEnumType enumValue)
            where TEnumType : struct
            where TAtributeType : Attribute
        {
            if (!typeof(TEnumType).IsEnum)
                throw new ArgumentException("Given value must be of enum type", "enumValue");

            var memberInfo = typeof(TEnumType).GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(TAtributeType), false);

                if (attrs.Length > 0)
                    return (TAtributeType)attrs[0];
            }

            return default;
        }

        public static string GetDescription<T>(this T enumValue)
            where T : struct
        {
            var descriptionAttribute = enumValue.GetAttribute<T, DescriptionAttribute>();
            return descriptionAttribute != null
                ? descriptionAttribute.Description
                : enumValue.ToString();
        }
    }
}
