using Spectre.Console;

namespace PhoneBook;

internal class UniquePropertyValidator<TKey, TModel> : IValidator
{
    public string ErrorMsg { get; set; } = "The Property must be unique.";
    public required Func<TKey, TModel?> GetModel { get; set; }
    public string? PropertyName { get; set; }
    public object[]? ExcludedValues { get; set; }

    public ValidationResult Validate(string input)
    {
        try
        {
            System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(TKey));
            if (converter != null && converter.CanConvertFrom(typeof(string)))
            {
                TKey key = (TKey)converter.ConvertFrom(input);
                TModel model = GetModel(key);

                if (model == null ||
                    (ExcludedValues != null
                        && PropertyName != null
                        && model != null
                        && ExcludedValues.Contains(model.GetType().GetProperty(PropertyName).GetValue(model))))
                {
                    return ValidationResult.Success();
                }
                return ValidationResult.Error($"[red]{ErrorMsg}[/]");
            }
            else
            {
                return ValidationResult.Error("[red]Input cannot be converted to the required key type.[/]");
            }
        }
        catch (Exception ex)
        {
            return ValidationResult.Error($"[red]Failed to convert input. {ex.Message}[/]");
        }
    }
}