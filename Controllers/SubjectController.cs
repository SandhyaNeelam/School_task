using Aspschool.DTOs;
using Aspschool.Models;
using Aspschool.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aspschool.Controllers;

[ApiController]
[Route("api/subject")]
public class SubjectController : ControllerBase
{
    

    private readonly ILogger<SubjectController> _logger;
    private readonly ISubjectRepository _subject;
    private readonly ITeacherRepository _teacher;


    public SubjectController(ILogger<SubjectController> logger, ISubjectRepository subject, ITeacherRepository teacher)
    {
        _logger = logger;
        _subject = subject;
        _teacher = teacher;
    }

    [HttpPost]
    public async Task<ActionResult<List<SubjectDTO>>> CreateSubject([FromBody] SubjectCreateDTO Data)
    {
        var toCreateSubject = new Subject
        {
            Name = Data.Name.Trim(),
    

        };
        var createdSubject = await _subject.Create(toCreateSubject);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ActionResult<List<SubjectDTO>>> GetAllSubjects()
    {
        var SubjectList = await _subject.GetList();
        var dtoList = SubjectList.Select(x => x.asDto);
        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectDTO>> GetSubjectById([FromRoute] int id)
    {
        var Subject = await _subject.GetById(id);
        if (Subject is null)
            return NotFound("No Subject is found with given id");
        var dto = Subject.asDto;
        dto.Teacher =(await _teacher.GetAllForSubject(id)).Select(x =>x.asDto).ToList();
        return Ok(dto);
    }



    





}
