using System.Text.RegularExpressions;

namespace Phonebook.wkktoria.Enums;

public static partial class EnumExtensions
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

    public partial class DisplayText : Attribute
    {
        public DisplayText(string text)
        {
            var words = UpperCaseRegex().Split(text);
            Text = string.Empty;

            foreach (var word in words) Text += word;
        }


        public string Text { get; }

        [GeneratedRegex("(?<!^)(?=[A-Z])")]
        private static partial Regex UpperCaseRegex();
    }
}