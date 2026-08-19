using LearningTracker.Api.Data;
using LearningTracker.models;
using Microsoft.EntityFrameworkCore;

namespace LearningTracker.Api.repositories;

public class EfLearningTopicRepository
    : LearningTopicRepository
{
    private readonly LearningTrackerDbContext _context;

    public EfLearningTopicRepository(
        LearningTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<LearningTopic>>
        GetAllAsync()
    {
        return await _context.LearningTopics
            .AsNoTracking()
            .OrderBy(topic => topic.Name)
            .ToListAsync();
    }

    public async Task<LearningTopic?> GetByIdAsync(Guid id)
    {
        return await _context.LearningTopics
            .FirstOrDefaultAsync(topic => topic.Id == id);
    }

    public async Task<LearningTopic?> GetByNameAsync(
        string name)
    {
        return await _context.LearningTopics
            .FirstOrDefaultAsync(topic =>
                topic.Name == name);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.LearningTopics
            .AnyAsync(topic => topic.Name == name);
    }

    public async Task AddAsync(LearningTopic topic)
    {
        await _context.LearningTopics.AddAsync(topic);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LearningTopic topic)
    {
        _context.LearningTopics.Update(topic);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(LearningTopic topic)
    {
        _context.LearningTopics.Remove(topic);

        await _context.SaveChangesAsync();
    }
}