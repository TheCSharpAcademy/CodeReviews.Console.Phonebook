global using Microsoft.EntityFrameworkCore;
using Phonebook.tonyissa.Models;

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