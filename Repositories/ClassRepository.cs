using Aspschool.Models;
using Dapper;

namespace Aspschool.Repositories;

public interface IClassRepository
{
    Task<Class> Create(Class Item);
  
    Task<Class> GetById(int Id);
    Task<List<Class>> GetList();

}

public class ClassRepository : BaseRepository, IClassRepository
{
    public ClassRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Class> Create(Class Item)
    {
        var query = $@"INSERT INTO class(class_capacity)
	VALUES (@ClassCapacity) RETURNING *";
    using (var connection = NewConnection)
    {
        var res= await connection.QuerySingleAsync<Class>(query, Item);
        return res;
    }
    }

    

    public async  Task<Class> GetById(int Id)
    {
        var query = $@"SELECT * FROM class WHERE id = @Id;";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Class>(query, new { Id });
    }

    public async Task<List<Class>> GetList()
    {
        var getQuery = $@"SELECT * FROM class";
        var connection = NewConnection;
        var res = (await connection.QueryAsync<Class>(getQuery)).AsList();
        await connection.CloseAsync();
        return res;
    }

    
}