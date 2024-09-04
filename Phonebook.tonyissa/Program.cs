global using Microsoft.EntityFrameworkCore;

void StartProgram()
{
    try
    {

    }
    catch(Exception ex)
    {
        Console.Write(ex.ToString());
        Console.WriteLine();
        Console.ReadKey();
        StartProgram();
    }
}

StartProgram();