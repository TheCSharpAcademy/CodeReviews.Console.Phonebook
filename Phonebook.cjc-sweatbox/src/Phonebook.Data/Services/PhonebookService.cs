using Phonebook.Data.Contexts;

namespace Phonebook.Data.Services;

/// <summary>
/// Partial class for non-entity specific database methods.
/// </summary>
public partial class PhonebookService
{
    #region Fields

    private readonly SqlDatabaseContext _context;

    #endregion
    #region Constructors

    public PhonebookService(SqlDatabaseContext context)
    {
        _context = context;
    }

    #endregion
}
