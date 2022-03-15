using Aspschool.Models;
using Dapper;

namespace Aspschool.Repositories;

public interface IStudentRepository
{
    Task<Student> Create(Student Item);
    Task<bool> Update(Student Item);
    Task<Student> GetById(int Id);
    Task<List<Student>> GetList();
    Task<List<Student>> GetAllForTeacher(int TeacherId );
    Task<List<Student>> GetAllForClass(int Id);
}

public class StudentRepository : BaseRepository, IStudentRepository
{
    public StudentRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Student> Create(Student Item)
    {
        var query = $@"INSERT INTO student(
	name, date_of_birth, gender, address, parent_mobile_num, class_id)
	VALUES (@Name, @DateOfBirth, @Gender, @Address, @ParentMobileNum, @ClassId) RETURNING *";
    using (var connection = NewConnection)
    {
        var res= await connection.QuerySingleAsync<Student>(query, Item);
        return res;
    }
    }

    public async Task<List<Student>> GetAllForClass(int Id)
    {
       var query = $@"SELECT * FROM student WHERE id = @Id";
       using (var connection = NewConnection)
    {
        return (await connection.QueryAsync<Student>(query, new{Id})).AsList();
         
    }
    }

    public async Task<List<Student>> GetAllForTeacher(int TeacherId)
    {
        var query = $@"SELECT * FROM student_teacher st LEFT JOIN student s ON s.id = st.student_id WHERE st.teacher_id = @TeacherId";
       using (var connection = NewConnection)
    {
        return ((await connection.QueryAsync<Student>(query, new{TeacherId})).AsList());
        
    }
    }

    public async  Task<Student> GetById(int Id)
    {
        var query = $@"SELECT * FROM student WHERE id = @Id;";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Student>(query, new { Id });
    }

    public async Task<List<Student>> GetList()
    {
        var getQuery = $@"SELECT * FROM student";
        var connection = NewConnection;
        var res = (await connection.QueryAsync<Student>(getQuery)).AsList();
        await connection.CloseAsync();
        return res;
    }

    public async Task<bool> Update(Student Item)
    {
         var updateQuery = $@"UPDATE student
	SET  date_of_birth=@DateOfBirth, address= @Address, parent_mobile_num=@ParentMobileNum,WHERE id =@Id";

        using (var connection = NewConnection)
        {

            var rowCount = await connection.ExecuteAsync(updateQuery, Item);
            return rowCount == 1;
        }
    }
}
