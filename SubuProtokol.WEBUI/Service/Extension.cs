using System.ComponentModel;
using System.Reflection;

namespace SubuProtokol.WEBUI.Service
{
    public static class Extensions
    {
        public static T To<T>(this string input) where T : struct
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("Input string is null or empty.");
            }

            try
            {
                T result = (T)Convert.ChangeType(input, typeof(T));
                return result;
            }
            catch (Exception)
            {
                throw new InvalidCastException($"Unable to convert string to {typeof(T).Name}.");
            }
        }

        public static T ToEnum<T>(this int value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return (Enum.IsDefined(typeof(T), value))
                ? (T)Enum.ToObject(typeof(T), value)
                : default(T);
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static IEnumerable<EnumDataType> ConvertEnumToDictionary<T>() where T : Enum
        {
            var enumType = typeof(T);
            var values = Enum.GetValues(enumType).Cast<int>();

            return values.Select(i =>
            {
                var member = enumType.GetMember(Enum.GetName(enumType, i))[0];
                var attribute = member.GetCustomAttribute<DescriptionAttribute>();
                return new EnumDataType
                {
                    Id = i,
                    Name = attribute != null ? attribute.Description : Enum.GetName(enumType, i)
                };
            });
        }

        public class EnumDataType
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
