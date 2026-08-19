using LearningTracker.models;
public interface LearningTopicService
{
    
    Task<LearningTopic> AddTopicAsync(string name);
    Task<IReadOnlyCollection<LearningTopic>> GetAllTopicsAsync();
    Task<LearningTopic?> FindByIdAsync(Guid id);
    Task<LearningTopic?> SearchByNameAsync(string name);
    Task<IReadOnlyCollection<LearningTopic>> GetCompletedTopicsAsync();
    Task UpdateTopicStatusAsync( Guid id,TopicStatus status);
    Task DeleteTopicAsync(Guid id);
}