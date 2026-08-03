namespace LearningTracker.models;

public class LearningTopic
{
    public Guid id { get;}
    public string Name { get; }
    public DateTime CreatedAt { get; }
    public bool IsCompleted { get; private set; }

    public LearningTopic(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }

        id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.Now;
        IsCompleted = false;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

}