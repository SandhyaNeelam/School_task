using Aspschool.Models;
using Dapper;

namespace Aspschool.Repositories;

public interface ITeacherRepository
{
    Task<Teacher> Create(Teacher Item);
    Task<bool> Update(Teacher Item);
    Task<Teacher> GetById(int Id);
    Task<List<Teacher>> GetList();
    Task<List<Teacher>> GetAllForStudent(int StudentId);
    Task<List<Teacher>> GetAllForSubject(int SubjectId);
   
}

public class TeacherRepository : BaseRepository, ITeacherRepository
{
    public TeacherRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Teacher> Create(Teacher Item)
    {
        var query = $@"INSERT INTO teacher(
	name,gendermobile,subject_id)
	VALUES (@Name, @Gender,@Mobile, @SubjectId) RETURNING *";
    using (var connection = NewConnection)
    {
        var res= await connection.QuerySingleAsync<Teacher>(query, Item);
        return res;
    }
    }


    public async  Task<Teacher> GetById(int Id)
    {
        // var query = $@"SELECT t.*, n AS name FROM teacher t
        // LEFT JOIN subject s ON s.id = t.subject_id
        // WHERE t.id = @Id";
        var query = $@"SELECT * FROM teacher WHERE id = @Id;";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Teacher>(query, new { Id });
    }

    public async Task<List<Teacher>> GetList()
    {
        var getQuery = $@"SELECT * FROM teacher";
        var connection = NewConnection;
        var res = (await connection.QueryAsync<Teacher>(getQuery)).AsList();
        await connection.CloseAsync();
        return res;
    }
    public async Task<bool> Update(Teacher Item)
    {
         var updateQuery = $@"UPDATE teacher
	SET mobile=@Mobile, subject_id= @SubjectId WHERE id =@Id";

        using (var connection = NewConnection)
        {

            var rowCount = await connection.ExecuteAsync(updateQuery, Item);
            return rowCount == 1;
        }
    }


    public async Task<List<Teacher>> GetAllForStudent(int StudentId)
    {
        var query= $@"SELECT t.*, s.name AS name FROM student_teacher st LEFT JOIN teacher t ON t.id= st.teacher_id
        LEFT JOIN subject s ON s.id = t.subject_id WHERE st.student_id = @StudentId" ;
         using (var connection = NewConnection)

            return (await connection.QueryAsync<Teacher>(query, new{StudentId})).AsList();
            
    
    }

    public async  Task<List<Teacher>> GetAllForSubject(int SubjectId)
    {
        var query= $@"SELECT t.*, s.name AS subject_name FROM teacher t LEFT JOIN subject s ON s.id = t.subject_id WHERE subject_id = @SubjectId";
        using (var connection = NewConnection)
         
            return (await connection.QueryAsync<Teacher>(query, new{SubjectId})).AsList();
    }

    
}