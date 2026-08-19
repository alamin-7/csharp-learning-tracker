using LearningTracker.models;

namespace LearningTracker.Api.dtos;

public record UpdateTopicStatusRequest(
    TopicStatus Status);