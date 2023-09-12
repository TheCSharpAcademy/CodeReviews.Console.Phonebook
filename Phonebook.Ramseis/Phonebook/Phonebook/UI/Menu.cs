using System.Net.Http.Headers;
using System.Numerics;

namespace Phonebook;
internal class Menu
{
    public string TopLeft { get; set; } = "╔";
    public string TopRight { get; set; } = "╛";
    public string Top { get; set; } = "═";
    public string Left { get; set; } = "║";
    public string Bottom { get; set; } = "═";
    public string BottomLeft { get; set; } = "╚";
    public string BottomRight { get; set; } = "╕";
    public string Right { get; set; } = "│";
    public string RightInnerTop { get; set; } = "┌┐";
    public string RightInnerBottom { get; set; } = "└┘";
    public string LeftHDivider { get; set; } = "╟";
    public string RightHDivider { get; set; } = "┤";
    public string HDivider { get; set; } = "─";
    public List<string> Options { get; set; } = new();
    public List<string> Titles { get; set; } = new();
    public List<string> Fields { get; set; } = new();
    List<int> FieldPosTop = new();
    public List<string> FieldString { get; set; } = new();
    public int FieldSize { get; set; } = 13;
    int FieldPosRight = 0;
    public int InputRow { get; set; }
    public int MinWidth { get; set; } = 50;
    public string Message { get; set; } = "";

    public void Draw()
    {
        Console.Clear();
        int maxStringLength = 0;
        int maxFieldLength = 0;
        foreach (string title in Titles)
        {
            if (maxStringLength < title.Length) { maxStringLength = title.Length; }
        }
        if (Fields.Count > 0)
        {
            foreach (string field in Fields)
            {
                if (maxStringLength < field.Length + FieldSize + 4) { maxStringLength = field.Length + FieldSize + 4; }
                if (maxFieldLength < field.Length) { maxFieldLength = field.Length; }
            }
            FieldPosRight = maxFieldLength + 4;
            if (FieldString.Count > 0)
            {
                foreach (string field in FieldString)
                {
                    if (maxStringLength < field.Length + FieldPosRight + 2) { maxStringLength = field.Length + FieldPosRight + 2; }
                }
            }
        }
        else
        {
            foreach (string option in Options)
            {
                if (maxStringLength < option.Length + 4) { maxStringLength = option.Length + 4; }
            }
        }
        if (maxStringLength < MinWidth - 6) { maxStringLength = MinWidth - 6; }

        int row = 0;

        // Top bar
        Console.SetCursorPosition(0, row);
        Console.Write(TopLeft);
        for (int i = 0; i < maxStringLength + 4; i++)
        {
            Console.Write(Top);
        }
        Console.Write(TopRight);
        row++;

        // Title rows and divider (if any)
        if (Titles.Count > 0)
        {
            for (int i = 0; i < Titles.Count; i++)
            {
                Console.SetCursorPosition(0, row);
                Console.Write(Left + " ");
                Console.Write(Titles[i]);
                Console.SetCursorPosition(maxStringLength + 4, row);
                if (i == 0)
                {
                    Console.Write(RightInnerTop);
                }
                else
                {
                    Console.Write(Right + Right);
                }
                row++;
            }
            Console.SetCursorPosition(0, row);
            Console.Write(LeftHDivider);
            for (int i = 0; i < maxStringLength + 3; i++)
            {
                Console.Write(HDivider);
            }
            Console.Write(RightHDivider + Right);
            row++;
        }

        // Fields and divider (if any)
        if (Fields.Count > 0)
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                FieldPosTop.Add(row);
                Console.SetCursorPosition(FieldPosRight - 2, FieldPosTop[i]);
                Console.Write(": ");
                if (FieldString.Count == Fields.Count)
                {
                    Console.Write(FieldString[i]);
                }
                else
                {
                    FieldString.Add("");
                }
                Console.SetCursorPosition(0, row);
                Console.Write(Left + " ");
                Console.Write(Fields[i]);
                Console.SetCursorPosition(maxStringLength + 4, row);
                Console.Write(Right + Right);
                row++;
            }

        }

        // Option rows and divider (if any AND no fields)
        else if (Options.Count > 0)
        {
            for (int i = 0; i < Options.Count; i++)
            {
                Console.SetCursorPosition(0, row);
                Console.Write(Left + " ");
                Console.Write(Options[i]);
                Console.SetCursorPosition(maxStringLength + 4, row);
                Console.Write(Right + Right);
                row++;
            }
            Console.SetCursorPosition(0, row);
            Console.Write(LeftHDivider);
            for (int i = 0; i < maxStringLength + 3; i++)
            {
                Console.Write(HDivider);
            }
            Console.Write(RightHDivider + Right);
            row++;
        }

        if (Fields.Count == 0)
        {
            // Input bar
            Console.SetCursorPosition(0, row);
            Console.Write(Left);
            Console.SetCursorPosition(maxStringLength + 4, row);
            Console.Write(RightInnerBottom);
            row++;
        }
        // Bottom bar
        Console.SetCursorPosition(0, row);
        Console.Write(BottomLeft);
        for (int i = 0; i < maxStringLength + 4; i++)
        {
            Console.Write(Bottom);
        }
        Console.Write(BottomRight);
        this.InputRow = row - 1;
        Console.SetCursorPosition(2, this.InputRow + 2);
        Console.Write(Message);
        Console.SetCursorPosition(2, this.InputRow);
    }

    public string GetFieldString(int id)
    {
        if (Fields.Count == 0 | id > Fields.Count)
        {
            return "";
        }
        else
        {
            Console.SetCursorPosition(FieldPosRight, FieldPosTop[id]);
            for (int i = 0; i < FieldString[id].Length; i++)
            {
                Console.Write(' ');
            }
            Console.SetCursorPosition(FieldPosRight, FieldPosTop[id]);
            string input = Console.ReadLine()?.Trim() ?? "";
            FieldString[id] = input;
            return input;
        }
    } public string UpdateFieldString(int id)
    {
        if (Fields.Count == 0 | id > Fields.Count)
        {
            return "";
        }
        else
        {
            Console.SetCursorPosition(FieldPosRight, FieldPosTop[id]);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(FieldString[id]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(FieldPosRight, FieldPosTop[id]);
            string input = Console.ReadLine()?.Trim() ?? "";
            FieldString[id] = input;
            return input;
        }
    }

    public int GetFieldInt(int id)
    {
        if (Fields.Count == 0 | id > Fields.Count)
        {
            return -1;
        }
        else
        {
            Console.SetCursorPosition(FieldPosRight, FieldPosTop[id]);
            for (int i = 0; i < FieldString[id].Length; i++)
            {
                Console.Write(' ');
            }
            Console.SetCursorPosition(FieldPosRight, FieldPosTop[id]);
            int.TryParse((Console.ReadLine() ?? string.Empty).Trim(), out int input);
            FieldString[id] = input.ToString();
            return input;
        }
    } public int UpdateFieldInt(int id)
    {
        if (Fields.Count == 0 | id > Fields.Count)
        {
            return -1;
        }
        else
        {
            Console.SetCursorPosition(FieldPosRight, FieldPosTop[id]);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(FieldString[id]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(FieldPosRight, FieldPosTop[id]);
            int.TryParse((Console.ReadLine() ?? string.Empty).Trim(), out int input);
            FieldString[id] = input.ToString();
            return input;
        }
    }
}