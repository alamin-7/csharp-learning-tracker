using LearningTracker.models;
public interface LearningTopicService
{
    Task AddTopicAsync(string name);
    Task<IReadOnlyCollection<LearningTopic>> GetAllTopicsAsync();
    Task<IReadOnlyCollection<LearningTopic>> GetCompletedTopicsAsync();
    Task StartTopicAsync(Guid id);
    Task UpdateTopicStatusAsync(
        string topicName,
        TopicStatus status);
    Task<LearningTopic?> SearchByNameAsync(string name);
}