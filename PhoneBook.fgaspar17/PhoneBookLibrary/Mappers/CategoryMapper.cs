namespace PhoneBookLibrary;

public static class CategoryMapper
{
    public static CategoryDto? MapToDto(Category category)
    {
        if (category == null) return null;
        return new CategoryDto { Name = category.Name };
    }
}