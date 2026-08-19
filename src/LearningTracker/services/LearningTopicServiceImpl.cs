using LearningTracker.models;
using LearningTracker.repositories;
public class LearningTopicServiceImpl
    : LearningTopicService
{

    private readonly LearningTopicRepository _repository;

    public LearningTopicServiceImpl(
        LearningTopicRepository repository)
    {
        _repository = repository;
    }
    public async Task<LearningTopic> AddTopicAsync(
    string name)
{
    bool exists =
        await _repository.ExistsByNameAsync(name);

    if (exists)
    {
        throw new InvalidOperationException(
            $"Topic '{name}' already exists.");
    }

    var topic = new LearningTopic(name);

    await _repository.AddAsync(topic);

    return topic;
}
public Task<IReadOnlyCollection<LearningTopic>> GetAllTopicsAsync()
{
    return _repository.GetAllAsync();
}
public Task<LearningTopic?> FindByIdAsync(Guid id)
{
    return _repository.GetByIdAsync(id);
}
public Task<LearningTopic?> SearchByNameAsync(
    string name)
{
    return _repository.GetByNameAsync(name);
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
public async Task UpdateTopicStatusAsync(
    Guid id,
    TopicStatus status)
{
    var topic =
        await _repository.GetByIdAsync(id);

    if (topic is null)
    {
        throw new InvalidOperationException(
            $"Topic '{id}' was not found.");
    }

    switch (status)
    {
        case TopicStatus.NotStarted:
            topic.Reset();
            break;

        case TopicStatus.InProgress:
            topic.Start();
            break;

        case TopicStatus.Completed:
            topic.Complete();
            break;

        default:
            throw new ArgumentOutOfRangeException(
                nameof(status));
    }

    await _repository.UpdateAsync(topic);
}

public async Task DeleteTopicAsync(Guid id)
{
    var topic =
        await _repository.GetByIdAsync(id);

    if (topic is null)
    {
        throw new InvalidOperationException(
            $"Topic '{id}' was not found.");
    }

    await _repository.DeleteAsync(topic);
}

}