using System.ComponentModel;
using System.Reflection;

namespace Lyzer.Common.Extensions
{
    public static class Enums
    {
        public static int? GetValueFromEnumDescription<TEnum>(string description) where TEnum : Enum
        {
            var type = typeof(TEnum);

            foreach (var field in type.GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && attribute.Description == description)
                {
                    return (int)field.GetValue(null)!;
                }
            }

            return null;
        }
    }
}