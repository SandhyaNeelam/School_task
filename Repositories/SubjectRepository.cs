using Aspschool.DTOs;
using Aspschool.Models;
using Dapper;

namespace Aspschool.Repositories;

public interface ISubjectRepository
{
    Task<Subject> Create(Subject Item);
    // Task<bool> Update(Subject Item);
    Task<Subject> GetById(int Id);
    Task<List<Subject>> GetList();
    Task<List<Subject>> GetAllForTeacher(int TeacherId);
    Task<List<Subject>> GetAllForStudent(int StudentId);
    


}

public class SubjectRepository : BaseRepository, ISubjectRepository
{
    public SubjectRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Subject> Create(Subject Item)
    {
        var query = $@"INSERT INTO subject(name) VALUES (@Name) RETURNING *";
        using (var connection = NewConnection)
        {
            var res = await connection.QuerySingleAsync<Subject>(query, Item);
            return res;
        }
    }



    public async Task<Subject> GetById(int Id)
    {
        var query = $@"SELECT * FROM subject WHERE id = @Id;";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Subject>(query, new { Id });
    }

    public async Task<List<Subject>> GetList()
    {
        var getQuery = $@"SELECT * FROM subject";
        var connection = NewConnection;
        var res = (await connection.QueryAsync<Subject>(getQuery)).AsList();
        await connection.CloseAsync();
        return res;
    }
    

    public async Task<List<Subject>> GetAllForTeacher(int TeacherId)
    {
        var query = $@"SELECT * FROM teacher WHERE id = @TeacherId;";
        using (var connection = NewConnection)
        return (await connection.QueryAsync<Subject>(query, new{TeacherId})).AsList();
            
        
    }

    public async Task<List<Subject>> GetAllForStudent(int StudentId)
    {
        var query = $@"SELECT * FROM student_subject ss LEFT JOIN subject s ON s.id =ss.subject_id WHERE ss.student_id = @StudentId";
        using (var connection = NewConnection)
        return (await connection.QueryAsync<Subject>(query, new{StudentId})).AsList();
    }





    // public async Task<bool> Update(Subject Item)
    // {
    //      var updateQuery = $@"UPDATE Subject
    // SET mobile=@Mobile, subject_id= @SubjectId WHERE id =@Id";

    //     using (var connection = NewConnection)
    //     {

    //         var rowCount = await connection.ExecuteAsync(updateQuery, Item);
    //         return rowCount == 1;
    //     }
    // }


}