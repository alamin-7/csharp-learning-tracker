using LearningTracker.models;
using LearningTracker.repositories;
public class LearningTopicServiceImpl
    : LearningTopicService
{
    private readonly List<LearningTopic> _topics = [];
    private readonly LearningTopicRepository _repository;

    public LearningTopicServiceImpl(
        LearningTopicRepository repository)
    {
        _repository = repository;
    }

    public async Task AddTopicAsync(string name)
    {
        var topics = ((await _repository.GetAllAsync())
            .ToList());

        bool exists = topics.Any(
            topic => topic.Name.Equals(
                name,
                StringComparison.OrdinalIgnoreCase));

            if (exists)
        {
            throw new InvalidOperationException(
                $"Topic '{name}' already exists.");
        }

        topics.Add(new LearningTopic(name));

        await _repository.SaveAllAsync(topics);        
    }
    public async Task<IReadOnlyCollection<LearningTopic>> GetAllTopicsAsync()
    {
        return await _repository.GetAllAsync();
    }
        public async Task<IReadOnlyCollection<LearningTopic>>
    GetCompletedTopicsAsync()
    {
        var topics = await _repository.GetAllAsync();

        return topics
            .Where(topic =>
                topic.Status == TopicStatus.Completed)
            .ToList();
    }
    public async Task StartTopicAsync(Guid id)
    {
        var topics = (await _repository.GetAllAsync()).ToList();

        var topic = topics.FirstOrDefault(topic =>
            topic.id == id);

        if (topic is null)
        {
            throw new InvalidOperationException(
                $"Topic with ID '{id}' was not found.");
        }

        topic.Start();

        await _repository.SaveAllAsync(topics);
    }
    public async Task UpdateTopicStatusAsync(
        string topicName,
        TopicStatus status)
    {
        var topics = (await _repository.GetAllAsync()).ToList();

        var topic = topics.FirstOrDefault(topic =>
            topic.Name.Equals(
                topicName,
                StringComparison.OrdinalIgnoreCase));

        if (topic is null)
        {
            throw new InvalidOperationException(
                $"Topic '{topicName}' was not found.");
        }

        switch (status)
        {
            case TopicStatus.InProgress:
                topic.Start();
                break;

            case TopicStatus.Completed:
                topic.UpdateStatus(TopicStatus.Completed);
                break;

            default:
                topic.UpdateStatus(TopicStatus.NotStarted);
                break;    
        }

        await _repository.SaveAllAsync(topics);
    }

    public async Task<LearningTopic?> SearchByNameAsync(
        string name)
    {
        var topics = await _repository.GetAllAsync();

        return topics.FirstOrDefault(topic =>
            topic.Name.Equals(
                name,
                StringComparison.OrdinalIgnoreCase));
    }
}