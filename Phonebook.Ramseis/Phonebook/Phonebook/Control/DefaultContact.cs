namespace Phonebook;

internal class DefaultContact
{
    public static void Load()
    {
        List<Contact> contacts = Controller.GetContacts();
        foreach (Contact contact in contacts)
        {
            Controller.DeleteContact(contact);
        }

        List<string> name = Shuffle(new List<string> {"Anthony","Barry","Charles","Drew","Elizabeth","Fred","Greg",
            "Heather","Ivan","Julie","Kassandra","Larry","Mike","Nathan","Oscar","Priscilla","Quin","Randy",
            "Sara","Theo","Urvine","Victoria","Willy","Xena","Yvette","Zach"});
        List<string> phone = Shuffle(new List<string>
        {
            "27-113-2926","813-293-7372","188-522-6542","458-813-0115","631-806-2423","551-504-3794","292-222-1142",
            "576-542-2419","577-226-3563","593-622-7751","656-976-4238","835-404-6601","753-831-8438","347-668-5704",
            "017-868-8223","347-668-5704","003-623-4606","475-061-1841","985-138-3313","699-461-7499","141-455-8095",
            "182-547-1252","153-848-8383","218-447-4237","067-566-5088","405-425-4130","284-492-1642"});
        List<string> address = Shuffle(new List<string>
        {
            "3298 Eu St.","547-4558 Vestibulum Ave","141-7903 Euismod Ave","Ap #657-186 Mi. Av.","P.O. Box 896, 3545 Ac Street",
            "7909 Non St.","P.O. Box 859, 6781 Erat St.","832-4477 Gravida. Street","P.O. Box 561, 1965 Quam, Rd.",
            "891-9669 Adipiscing St.","Ap #322-336 Tempus Av.","Ap #826-8355 Sit St.","219-9134 Metus. Rd.","393-1455 In, Rd.",
            "995 Auctor Street","4367 Non, Ave","P.O. Box 761, 7405 Ac Rd.","952-3562 Tellus Road","Ap #254-223 Tincidunt Road",
            "Ap #132-2456 At Road","3705 At Rd.","6611 Sapien, Ave","849-1148 Dui Av.","Ap #353-6679 Dictum Street",
            "P.O. Box 978, 139 Mattis Rd.","Ap #439-8726 Amet St."});
        List<string> city = Shuffle(new List<string> {
            "Cambridge","Colorado Springs","Pittsburgh","Minneapolis","Cambridge","Toledo","Kaneohe","Iowa City",
            "Hillsboro","Pocatello","Seattle","Jefferson City","Colorado Springs","Austin","Lowell","Hattiesburg",
            "Evansville","Lawton","Olympia","Pike Creek","Boston","Evansville","Saint Paul","Fort Collins","Bridgeport","Detroit"});
        List<string> state = Shuffle(new List<string> {
            "Idaho","Illinois","Hawaii","Indiana","Wisconsin","Nebraska","Nebraska","Arizona","Idaho","Montana","Massachusetts",
            "Colorado","Nevada","Wisconsin","Illinois","California","Arkansas","Illinois","Nebraska","Georgia","Hawaii","Minnesota",
            "Minnesota","Kentucky","Connecticut","Minnesota","Connecticut","Arkansas","Washington","Idaho"});
        List<int> zip = Shuffle(new List<int>
        {
            36398,61743,64128,33545,32514,39232,77224,87316,36678,35273,83174,38930,27417,56614,35787,42283,84508,27828,
            84840,52449,41478,56613,43587,38578,11123,35486});
        List<string> email = Shuffle(new List<string> {
            "ligula@yahoo.net","dapibus.quam@hotmail.org","donec.egestas.duis@protonmail.net","laoreet.ipsum@google.com",
            "nulla.facilisi@icloud.couk","aliquet@hotmail.net","metus.urna@icloud.org","facilisis.vitae@hotmail.ca",
            "nunc@icloud.ca","adipiscing.fringilla@protonmail.com","facilisis.magna@aol.net","pellentesque.ultricies@aol.couk",
            "sit.amet@hotmail.ca","cum.sociis.natoque@yahoo.ca","vel@outlook.org","et@yahoo.edu","eleifend.vitae@yahoo.ca",
            "eros@google.org","lorem.ipsum@google.ca","tempus@aol.couk","placerat.orci.lacus@aol.com","ac.libero@hotmail.org",
            "enim@icloud.org","urna@aol.org","donec.elementum@protonmail.couk","congue.in@aol.ca","euismod@outlook.com",
            "pede.blandit@protonmail.ca","sit.amet@protonmail.net","pede.suspendisse@aol.net",});
        List<string> group = Shuffle(new List<string> {
            "Friend","Friend","Friend","Friend","Friend","Work","Work","Work","Work","Work","Excercise","Excercise",
            "Excercise","Excercise","Excercise","Utility","Utility","Utility","Utility","Utility","Event","Event",
            "Event","Event","Event"});
        Random rng = new Random();
        DateTime startDay = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
        for (int i = 0; i < 24; i++)
        {
            Controller.AddContact(new Contact
            {
                Name = name[i],
                Phone = phone[i],
                Address = address[i],
                City = city[i],
                State = state[i],
                ZipCode = zip[i],
                Email = email[i],
                Group = group[i],
                LastAccess = startDay.AddMonths(rng.Next(12)).AddDays(rng.Next(28)).AddHours(rng.Next(24)).AddMinutes(rng.Next(60))
            });
        }
    }

    private static List<T> Shuffle<T>(List<T> list)
    {
        int i = list.Count;
        Random rng = new Random();
        while (i > 1)
        {
            i--;
            int rand = rng.Next(i + 1);
            T value = list[rand];
            list[rand] = list[i];
            list[i] = value;
        }
        return list;
    }
}
