List<string> topics = [];

bool running = true;

while (running)
{
    ShowMenu();

    string? option = Console.ReadLine();

    switch (option)
    {
        case "1":
            AddTopic(topics);
            break;

        case "2":
            ShowTopics(topics);
            break;

        case "3":
            running = false;
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

static void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("===== Learning Tracker =====");
    Console.WriteLine("1. Add Topic");
    Console.WriteLine("2. List Topics");
    Console.WriteLine("3. Exit");
    Console.Write("Select: ");
}

static void AddTopic(List<string> topics)
{
    Console.Write("Topic: ");

    string? topic = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(topic))
    {
        Console.WriteLine("Topic cannot be empty.");
        return;
    }

    topics.Add(topic);

    Console.WriteLine("Topic added.");
}

static void ShowTopics(List<string> topics)
{
    if (topics.Count == 0)
    {
        Console.WriteLine("No topics found.");
        return;
    }

    foreach (string topic in topics)
    {
        Console.WriteLine($"- {topic}");
    }
}