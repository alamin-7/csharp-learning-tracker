using LearningTracker.models;
public class LearningTopicServiceImpl
    : LearningTopicService
{
    private readonly List<LearningTopic> _topics = [];

    public void AddTopic(string name)
    {
        if (_topics.Any(topic =>
            topic.Name.Equals(
                name,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "Topic already exists.");
        }

        _topics.Add(
            new LearningTopic(name));
    }

    public IReadOnlyCollection<LearningTopic>
        GetAllTopics()
    {
        return _topics;
    }

    public LearningTopic? FindById(Guid id)
    {
        return _topics.FirstOrDefault(
            topic => topic.id == id);
    }

    public IReadOnlyCollection<LearningTopic> GetCompletedTopics()
    {
        return _topics.Where(
            topic => topic.Status == TopicStatus.Completed)
            .ToList();
    }

    public void StartTopic(Guid id)
    {
        LearningTopic? topic =
            FindById(id);

        topic?.Start();
    }

    public void UpdateTopicStatus(string topicName, TopicStatus status)
{
    var topic = SearchByName(topicName);

    if (topic is null)
    {
        throw new InvalidOperationException("Topic not found.");
    }

    switch (status)
    {
        case TopicStatus.NotStarted:
            topic.Start();     
            break;

        case TopicStatus.InProgress:
            topic.Start();
            break;

        case TopicStatus.Completed:
            topic.UpdateStatus(TopicStatus.Completed);
            break;
    }
}
    public LearningTopic? SearchByName(string name)
    {
        return _topics.FirstOrDefault(
            topic => topic.Name.Equals(
                name,
                StringComparison.OrdinalIgnoreCase));
    }
}