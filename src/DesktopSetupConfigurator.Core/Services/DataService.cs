using System.Linq.Expressions;
using System.Text.Json;
using DesktopSetupConfigurator.Database;
using DesktopSetupConfigurator.Database.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DesktopSetupConfigurator.Core.Services;

public sealed class DataService : IDataService
{
    private readonly IDbContextFactory<CommandsDbContext> _dbContextFactory;

    public DataService(IDbContextFactory<CommandsDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory!;
    }

    public async Task Setup(bool createDummyData = true)
    {
        var conn = await _dbContextFactory.CreateDbContextAsync();
        await conn.Database.EnsureDeletedAsync();

        await conn.Database.EnsureCreatedAsync();

        if (createDummyData)
        {
            if (!conn.InstallationEnvironments.Any())
            {
                conn.InstallationEnvironments.AddRange(
                    new()
                    {
                        Name = "any",
                        Description = "can be executed everywhere",
                        IsDefault = true,
                    },
                    new()
                    {
                        Name = "win",
                        Description = "Windows"
                    },
                    new()
                    {
                        Name = "lx",
                        Description = "Linux"
                    });
            }

            if (!conn.InstallationProfiles.Any())
            {
                conn.InstallationProfiles.AddRange(
                    new()
                    {
                        Name = "any",
                        IsDefault = true,
                    },
                    new() { Name = "work" },
                    new() { Name = "any" });
            }

            if (!conn.InstallationStages.Any())
            {
                conn.InstallationStages.AddRange(
                    new() { Name = "pre-install" },
                    new() { Name = "install", IsDefault = true },
                    new() { Name = "post-Install" });
            }

            if (!conn.Commands.Any())
            {
                conn.Commands.AddRange(
                    new()
                    {
                        Text = "winget install --id Git.Git",
                        Stage = conn.InstallationStages.First(x => x.Name == "win"),
                        Environments = conn.InstallationEnvironments.Where(x => x.Name == "install").ToArray(),
                        Profiles = conn.InstallationProfiles.Where(x => x.Name == "any").ToArray()
                    },
                    new()
                    {
                        Text = "winget install --id Microsoft.VisualStudioCode",
                        Stage = conn.InstallationStages.First(x => x.Name == "win"),
                        Environments = conn.InstallationEnvironments.Where(x => x.Name == "install").ToArray(),
                        Profiles = conn.InstallationProfiles.Where(x => x.Name == "any").ToArray()
                    },
                    new()
                    {
                        Text = "winget install --id Microsoft.PowerShell",
                        Stage = conn.InstallationStages.First(x => x.Name == "win"),
                        Environments = conn.InstallationEnvironments.Where(x => x.Name == "install").ToArray(),
                        Profiles = conn.InstallationProfiles.Where(x => x.Name == "any").ToArray()
                    });
            }
        }

        var json = JsonSerializer.Serialize(new
        {
            CreatedOn = DateTime.Now,
        });
        File.WriteAllText(Directory.GetCurrentDirectory() + "/appInfo.json", json);
    }

    public async Task Init()
    {
        var conn = await _dbContextFactory.CreateDbContextAsync();
        await conn.Database.MigrateAsync();
    }

    public async Task<IQueryable<T>> GetAll<T>(Func<T, bool>? wherePredicate = null)
    where T : DbModel
    {
        var conn = await _dbContextFactory.CreateDbContextAsync();

        var set = conn.Set<T>();
        var items = wherePredicate is null
        ? set.Where(x => x.IsDeleted == false)
        : set.Where(wherePredicate);
        return items.AsQueryable();
    }

    public async Task<T> GetAsync<T>(Expression<Func<T, bool>> wherePredicate)
    where T : DbModel
    {
        var conn = await _dbContextFactory.CreateDbContextAsync();

        var item = await conn.Set<T>().FirstAsync(predicate: wherePredicate);
        return item;
    }

    public async Task AddAsync<T>(T entity)
    where T : DbModel
    {
        var conn = await _dbContextFactory.CreateDbContextAsync();

        await conn.Set<T>().AddAsync(entity);
        await conn.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(T entity)
    where T : DbModel
    {
        var conn = await _dbContextFactory.CreateDbContextAsync();
        var item = await conn.Set<T>().FirstAsync(x => x.Id == entity.Id);
        item.Delete();
        await conn.SaveChangesAsync();
    }

    public async Task ModifyAsync<T>(T entity)
    where T : DbModel
    {
        var conn = await _dbContextFactory.CreateDbContextAsync();
        var item = await conn.Set<T>().FirstAsync(x => x.Id == entity.Id);
        item = entity;
        item.Update();
        await conn.SaveChangesAsync();
    }

}
