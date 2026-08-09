using LearningTracker.models;
using LearningTracker.repositories;

bool running = true;
string filePath = Path.Combine("data", "topics.json");

LearningTopicRepository repository =
    new JsonLearningTopicRepository(filePath);

LearningTopicService topicService =
    new LearningTopicServiceImpl(repository);

while (running)
{
    ShowMenu();

    string? option = Console.ReadLine();

    switch (option)
    {
        case "1":
            await addTopicAsync(topicService);
            break;

        case "2":
            await ShowTopicsAsync(topicService);
            break;

        case "3":
            await UpdateTopicStatusAsync(topicService);
            break;

        case "4":
            running = false;
            break;    

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

async Task addTopicAsync(LearningTopicService topicService)
{
    Console.Write("Enter topic name: ");
    string? name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Topic name cannot be empty.");
        return;
    }

    try
    {
        await topicService.AddTopicAsync(name);
        Console.WriteLine($"Topic '{name}' added successfully.");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

async Task showCompletedTopics(LearningTopicService topicService)
{
    var completedTopics = await topicService.GetCompletedTopicsAsync();

    if (!completedTopics.Any())
    {
        Console.WriteLine("No completed topics found.");
        return;
    }

    Console.WriteLine("Completed Topics:");
    foreach (var topic in completedTopics)
    {
        Console.WriteLine($"- {topic.Name} (Status: {topic.Status})");
    }
}

async Task ShowTopicsAsync(LearningTopicService topicService)
{
    var topics = await topicService.GetAllTopicsAsync();

    if (!topics.Any())
    {
        Console.WriteLine("No topics found.");
        return;
    }

    Console.WriteLine("Topics:");
    foreach (var topic in topics)
    {
        Console.WriteLine($"- {topic.Name} (Status: {topic.Status})");
    }
}

async Task UpdateTopicStatusAsync(LearningTopicService topicService)
{
    Console.Write("Enter topic name: ");
    string? topicName = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(topicName))
    {
        Console.WriteLine("Topic name is required.");
        return;
    }

    var topic = await topicService.SearchByNameAsync(topicName);

    if (topic is null)
    {
        Console.WriteLine("Topic not found.");
        return;
    }

    Console.WriteLine($"Current status: {topic.Status}");
    Console.WriteLine();
    Console.WriteLine("Select new status:");
    Console.WriteLine("1. Planned");
    Console.WriteLine("2. In Progress");
    Console.WriteLine("3. Completed");
    Console.Write("Select: ");

    string? statusInput = Console.ReadLine();

    TopicStatus newStatus;

    switch (statusInput)
    {
        case "1":
            newStatus = TopicStatus.NotStarted;
            break;

        case "2":
            newStatus = TopicStatus.InProgress;
            break;

        case "3":
            newStatus = TopicStatus.Completed;
            break;

        default:
            Console.WriteLine("Invalid status.");
            return;
    }

    await topicService.UpdateTopicStatusAsync(topic.Name, newStatus);

    Console.WriteLine($"'{topic.Name}' updated to {newStatus}.");
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