using LearningTracker.models;
using Microsoft.EntityFrameworkCore;

namespace LearningTracker.Api.Data;

public class LearningTrackerDbContext : DbContext
{
    public LearningTrackerDbContext(
        DbContextOptions<LearningTrackerDbContext> options)
        : base(options)
    {
    }

    public DbSet<LearningTopic> LearningTopics =>
        Set<LearningTopic>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LearningTopic>()
            .HasKey(topic => topic.Id);
    }    
}