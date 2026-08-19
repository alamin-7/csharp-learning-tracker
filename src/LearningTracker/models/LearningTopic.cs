namespace LearningTracker.models;

public class LearningTopic
{

    public Guid Id { get;}
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; }
    public TopicStatus Status { get; private set; }

     private LearningTopic()
    {
    }

    public LearningTopic(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }

        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.Now;
        Status = TopicStatus.NotStarted;
    }


    public void Start()
    {
        if (Status == TopicStatus.Completed)
        {
            throw new InvalidOperationException(
                "A completed topic cannot be started again.");
        }

        Status = TopicStatus.InProgress;
    }

    public void UpdateStatus(TopicStatus status)
    {
        Status = status;
    }
    public void Complete()
    {
        Status = TopicStatus.Completed;
    }
    public void Reset()
    {
        Status = TopicStatus.NotStarted;
    }

}