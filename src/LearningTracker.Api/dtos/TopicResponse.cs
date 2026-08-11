using LearningTracker.models;
public record TopicResponse(
    Guid Id,
    string Name,
    TopicStatus Status
    );