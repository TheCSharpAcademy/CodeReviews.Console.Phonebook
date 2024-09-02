using System.ComponentModel;

namespace PhoneBook.Extensions;

/// <summary>
/// Provides extension methods for working with enumeration types.
/// </summary>
internal static class EnumTypeExtensions
{
    /// <summary>
    /// Retrieves the description from the <see cref="DescriptionAttribute"/> of an enum type.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <returns>The description of the enum type if it exists; otherwise, the name of the enum type.</returns>
    public static string GetEnumDescription<TEnum>() where TEnum : Enum
    {
        Type enumType = typeof(TEnum);
        DescriptionAttribute? attribute = enumType.GetCustomAttribute<DescriptionAttribute>();

        return attribute is null ? enumType.Name : attribute.Description;
    }
}