using LearningTracker.models;
public interface LearningTopicRepository
{
    
    Task<IReadOnlyCollection<LearningTopic>> GetAllAsync();

    Task<LearningTopic?> GetByIdAsync(Guid id);

    Task<LearningTopic?> GetByNameAsync(string name);

    Task AddAsync(LearningTopic topic);

    Task UpdateAsync(LearningTopic topic);

    Task DeleteAsync(LearningTopic topic);

    Task<bool> ExistsByNameAsync(string name);
}