namespace PhoneBook;

class Controller
{
    public void ShowMenu()
    {
        var view = new MenuView(this);
        view.Show();
    }

    public void ShowList()
    {
        var view = new ListView(this);
        view.Show();
    }

    public void ShowAdd()
    {
        var view = new AddView(this);
        view.Show();
    }

    public void ShowEdit()
    {
        var view = new EditView(this);
        view.Show();
    }

    public void ShowDelete()
    {
        var view = new DeleteView(this);
        view.Show();
    }

    public void ShowExit()
    {
        var view = new ExitView();
        view.Show();
    }
}