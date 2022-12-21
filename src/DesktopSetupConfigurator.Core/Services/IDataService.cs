using System.Linq.Expressions;
using DesktopSetupConfigurator.Database.Models.Common;

namespace DesktopSetupConfigurator.Core.Services;
public interface IDataService
{
    Task Setup(bool createDummyData = true);
    Task Init();
    Task<IQueryable<T>> GetAll<T>(Func<T, bool>? wherePredicate = null)
    where T : DbModel;
    Task<T> GetAsync<T>(Expression<Func<T, bool>> wherePredicate)
    where T : DbModel;
    Task AddAsync<T>(T entity)
    where T : DbModel;
    Task DeleteAsync<T>(T entity)
    where T : DbModel;
    Task ModifyAsync<T>(T entity)
    where T : DbModel;
}
