using LearningTracker.models;
public interface LearningTopicRepository
{
    
    Task<IReadOnlyCollection<LearningTopic>> GetAllAsync();

    Task SaveAllAsync(IEnumerable<LearningTopic> topics);
}