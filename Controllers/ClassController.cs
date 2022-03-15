using Aspschool.DTOs;
using Aspschool.Models;
using Aspschool.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aspschool.Controllers;

[ApiController]
[Route("api/class")]


public class ClassController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IClassRepository _classes;
    private readonly IStudentRepository _student;

    public ClassController(ILogger<StudentController> logger, IClassRepository classes, IStudentRepository student)
    {
        _logger = logger;
        _classes =classes;
        _student= student;
    }

    [HttpPost]
    public async Task<ActionResult<List<ClassDTO>>> CreateClass([FromBody] ClassCreateDTO Data)
    {
        var toCreateClass = new Class
        {
            ClassCapacity= Data.ClassCapacity

        };
       var createdClass = await _classes.Create(toCreateClass);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ActionResult<List<ClassDTO>>> GetAllClasses()
    {
        var ClassList = await _classes.GetList();
        var dtoList = ClassList.Select(x => x.asDto);
        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClassDTO>> GetClassById([FromRoute] int id)
    {
        var classes = await _classes.GetById(id);
        if (classes is null)
            return NotFound("No class is found with given id");
        var dto = classes.asDto;
        dto.Student = (await _student.GetAllForClass(id)).Select(x =>x.asDto).ToList();
        return Ok(dto);
    }


    

    
 















}