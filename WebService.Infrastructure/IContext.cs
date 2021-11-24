﻿namespace WebService.Infrastructure
{
    public interface IContext : IDisposable
    {
        DbSet<Language> Languages { get; }
        DbSet<Level> Levels { get; }
        DbSet<Material> Materials { get; }
        DbSet<Media> Medias { get; }
        DbSet<ProgrammingLanguage> ProgrammingLanguages { get; }
        DbSet<Rating> Ratings { get; }
        DbSet<Tag> Tags { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
