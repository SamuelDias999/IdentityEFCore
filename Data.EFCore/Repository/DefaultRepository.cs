using System.Linq.Expressions;
using Data.EFCore.Banco;

namespace Data.EFCore.Repository;

public class DefaultRepository
{
    private readonly AppDbContext _context;

    public DefaultRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public IEnumerable<T> GetAll<T>() where T : class
    {
        return _context.Set<T>().ToList();
    }

    public List<T> RetrieveBy<T>(Expression<Func<T,bool>> condition) where T : class
    {
        return _context.Set<T>().Where(condition).ToList();
    }
}