using LearningTracker.models;
using System.Text.Json;

namespace LearningTracker.repositories;
public class JsonLearningTopicRepository : LearningTopicRepository
{
    private readonly string _filePath;

    public JsonLearningTopicRepository(string filePath)
    {
        _filePath = filePath;
    }

      public async Task<IReadOnlyCollection<LearningTopic>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        string json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<LearningTopic>>(json) ?? [];
    }

    public async Task SaveAllAsync(IEnumerable<LearningTopic> topics)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(topics, options);

        await File.WriteAllTextAsync(_filePath, json);
    }
}