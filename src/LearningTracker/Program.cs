using LearningTracker.models;

List<LearningTopic> topics =[];


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
    Console.WriteLine("3. Mark Topic as Completed");
    Console.WriteLine("4. Exit");
    Console.Write("Select: ");
}

static void AddTopic(List<LearningTopic> topics)
{
    Console.Write("Topic: ");
    string? topicName = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(topicName))
    {
        Console.WriteLine("Topic name is required.");
        return;
    }

    LearningTopic topic = new LearningTopic(topicName);

    topics.Add(topic);

    Console.WriteLine("Topic added.");
}

static void ShowTopics(List<LearningTopic> topics)
{
    if (topics.Count == 0)
    {
        Console.WriteLine("No topics found.");
        return;
    }

    foreach (LearningTopic topic in topics)
    {
        Console.WriteLine(
            $"{topic.id} | {topic.Name} | Completed: {topic.IsCompleted}"
        );
    }
}

static void CompleteTopic(List<LearningTopic> topics)
{
    Console.Write("Enter topic ID: ");
    string? input = Console.ReadLine();

    if (!Guid.TryParse(input, out Guid topicId))
    {
        Console.WriteLine("Invalid topic ID.");
        return;
    }

    LearningTopic? topic = topics
        .FirstOrDefault(topic => topic.id == topicId);

    if (topic is null)
    {
        Console.WriteLine("Topic not found.");
        return;
    }

    topic.MarkAsCompleted();

    Console.WriteLine("Topic completed.");
}