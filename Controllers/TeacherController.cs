using Aspschool.DTOs;
using Aspschool.Models;
using Aspschool.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aspschool.Controllers;

[ApiController]
[Route("api/teacher")]
public class TeacherController : ControllerBase
{


    private readonly ILogger<TeacherController> _logger;
    private readonly ITeacherRepository _teacher;
    private readonly IStudentRepository _student;

    public TeacherController(ILogger<TeacherController> logger, ITeacherRepository teacher, IStudentRepository student)
    {
        _logger = logger;
        _teacher = teacher;
        _student = student;
    }

    [HttpPost]
    public async Task<ActionResult<List<TeacherDTO>>> CreateTeacher([FromBody] TeacherCreateDTO Data)
    {
        var toCreateTeacher = new Teacher
        {
            Name = Data.Name.Trim(),
            Gender = Data.Gender.Trim(),
            Mobile = Data.Mobile,
            SubjectId = Data.SubjectId

        };
        var createdTeacher = await _teacher.Create(toCreateTeacher);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ActionResult<List<TeacherDTO>>> GetAllTeachers()
    {
        var TeacherList = await _teacher.GetList();
        var dtoList = TeacherList.Select(x => x.asDto);
        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherDTO>> GetTeacherById([FromRoute] int id)
    {
        var Teacher = await _teacher.GetById(id);
        if (Teacher is null)
            return NotFound("No Teacher is found with given id");
        var dto = Teacher.asDto;
        dto.Student = (await _student.GetAllForTeacher(id)).Select(x => x.asDto).ToList();
        return Ok(dto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeacher([FromRoute] int id, [FromBody] TeacherUpdateDTO Data)
    {
        var existing = await _teacher.GetById(id);
        if (existing is null)
            return NotFound("No Teacher found with given id");

        var toUpdateTeacher = existing with
        {

            Mobile = Data.Mobile,
            SubjectId = Data.SubjectId


        };

        var didUpdate = await _teacher.Update(toUpdateTeacher);
        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Teacher");

        return NoContent();

    }









}
