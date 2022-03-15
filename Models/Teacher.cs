using Aspschool.DTOs;

namespace Aspschool.Models;

public record Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public long Mobile { get; set; }
    public int SubjectId { get; set; }


    public TeacherDTO asDto => new TeacherDTO
    {
        Id= Id,
        Name= Name,
        Gender= Gender,
        Mobile= Mobile,
        SubjectId = SubjectId

    };











}