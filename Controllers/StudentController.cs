using Aspschool.DTOs;
using Aspschool.Models;
using Aspschool.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aspschool.Controllers;

[ApiController]
// [Route("[controller]")]
[Route("api/student")]
public class StudentController : ControllerBase
{

    private readonly ILogger<StudentController> _logger;
    private readonly IStudentRepository _student;
    private readonly ITeacherRepository _teacher;
    private readonly ISubjectRepository _subject;

    public StudentController(ILogger<StudentController> logger, IStudentRepository student, ITeacherRepository teacher, ISubjectRepository subject)
    {
        _logger = logger;
        _student = student;
        _teacher = teacher;
        _subject = subject;
    }

    [HttpPost]
    public async Task<ActionResult<List<StudentDTO>>> CreateStudent([FromBody] StudentCreateDTO Data)
    {
        var toCreateStudent = new Student
        {
            Name = Data.Name.Trim(),
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Gender = Data.Gender.Trim(),
            Address = Data.Address.Trim(),
            ParentMobileNum = Data.ParentMobileNum,
            ClassId = Data.ClassId

        };
        var createdStudent = await _student.Create(toCreateStudent);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentDTO>>> GetAllStudents()
    {
        var studentList = await _student.GetList();
        var dtoList = studentList.Select(x => x.asDto);
        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> GetStudentById([FromRoute] int id)
    {
        var student = await _student.GetById(id);
        if (student is null)
            return NotFound("No student is found with given id");
        var dto = student.asDto;

        dto.Teacher = (await _teacher.GetAllForStudent(id)).Select(x => x.asDto).ToList();
        dto.Subject = (await _subject.GetAllForStudent(id)).Select(x => x.asDto).ToList();
        return Ok(dto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent([FromRoute] int id, [FromBody] StudentUpdateDTO Data)
    {
        var existing = await _student.GetById(id);
        if (existing is null)
            return NotFound("No student found with given id");

        var toUpdateStudent = existing with
        {
            DateOfBirth = Data.DateOfBirth,
            Address = Data.Address.Trim(),
            ParentMobileNum = Data.ParentMobileNum
            //ParentMobileNum = Data.ParentMobileNum ?? existing.ParentMobileNum


        };

        var didUpdate = await _student.Update(toUpdateStudent);
        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update student");

        return NoContent();

    }







}













