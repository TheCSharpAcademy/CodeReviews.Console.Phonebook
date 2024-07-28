using System.ComponentModel;

namespace Phonebook.Extensions;

/// <summary>
/// System.Enum class extension methods.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the ComponentModel.Description for an enum. Useful for showing a user friendly string instead of the actual enum name.
    /// </summary>
    /// <param name="value">The enum value to get a description for.</param>
    /// <returns>Returns the enum descripion if it has one, otherwise the enum as string.</returns>
    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo!.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        return (attributes != null && attributes.Length > 0) ? attributes.First().Description : value.ToString();
    }
}
