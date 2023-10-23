using System.Text.RegularExpressions;

namespace Phonebook.wkktoria.Enums;

public static class EnumExtensions
{
    public static string ToDescription(this Enum e)
    {
        var type = e.GetType();

        var memInfo = type.GetMember(e.ToString());

        if (memInfo.Length <= 0) return e.ToString();

        var attrs = memInfo[0].GetCustomAttributes(
            typeof(DisplayText),
            false);

        return attrs.Length > 0 ? ((DisplayText)attrs[0]).Text : e.ToString();
    }

    public class DisplayText : Attribute
    {
        public DisplayText(string text)
        {
            var words = Regex.Split(text, @"(?<!^)(?=[A-Z])");
            Text = string.Empty;

            foreach (var word in words) Text += word;
        }


        public string Text { get; set; }
    }
}