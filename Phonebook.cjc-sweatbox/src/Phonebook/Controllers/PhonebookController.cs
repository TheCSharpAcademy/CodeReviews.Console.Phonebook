using Phonebook.Data.Contexts;
using Phonebook.Data.Services;

namespace Phonebook.Controllers;

/// <summary>
/// Partial class for non-specific model-database methods.
/// </summary>
public partial class PhonebookController
{
    #region Fields

    private readonly PhonebookService _service;

    #endregion
    #region Constructors

    public PhonebookController(SqlDatabaseContext context)
    {
        _service = new PhonebookService(context);
    }

    #endregion
}
