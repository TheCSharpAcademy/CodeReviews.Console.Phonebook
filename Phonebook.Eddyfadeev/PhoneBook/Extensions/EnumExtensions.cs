namespace PhoneBook.Extensions;

/// <summary>
/// The EnumExtensions class provides extension methods for working with enums.
/// </summary>
internal static class EnumExtensions
{
    /// <summary>
    /// Retrieves the description of the specified enum value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="enumValue">The enum value.</param>
    /// <returns>The description of the enum value.</returns>
    internal static string GetDescription<TEnum>(this TEnum enumValue)
        where TEnum : Enum
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());

        if (field is null)
        {
            return enumValue.ToString();
        }
        
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        
        return attribute is not null ? attribute.Description : enumValue.ToString();
    }

    /// <summary>
    /// Retrieves the enum value that corresponds to the specified description.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="description">The description to match with an enum value.</param>
    /// <returns>The enum value that corresponds to the specified description.</returns>
    /// <exception cref="ArgumentException">Thrown when the description does not match any enum value.</exception>
    internal static TEnum GetEnumValueFromDescription<TEnum>(string description)
        where TEnum : struct, Enum
    {
        foreach (var value in Enum.GetValues<TEnum>())
        {
            if (value.GetDescription() == description)
            {
                return value;
            }
        }

        throw new ArgumentException("The specified description does not exist in the enum.", nameof(description));
    }
}