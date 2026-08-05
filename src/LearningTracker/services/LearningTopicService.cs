using LearningTracker.models;
public interface LearningTopicService
{
    void AddTopic(string name);
    IReadOnlyCollection<LearningTopic> GetAllTopics();
    LearningTopic? FindById(Guid id);
    IReadOnlyCollection<LearningTopic> GetCompletedTopics();
    void StartTopic(Guid id);
    void UpdateTopicStatus(string topicName, TopicStatus status);
    LearningTopic? SearchByName(string name);
}