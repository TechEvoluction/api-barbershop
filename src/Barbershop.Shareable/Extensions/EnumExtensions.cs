using System.ComponentModel;
using System.Reflection;

namespace Barbershop.Shareable.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this System.Enum enumValue)
    {
        FieldInfo? field = enumValue.GetType().GetField(enumValue.ToString());

        if (field is null)
            return enumValue.ToString();

        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute is not null
            ? attribute.Description
            : enumValue.ToString();
    }
}