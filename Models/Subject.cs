using Aspschool.DTOs;

namespace Aspschool.Models;

public record Subject
{
    public int Id { get; set; }
    public string Name { get; set; }

    public SubjectDTO asDto => new SubjectDTO
    {
        Id= Id,
        Name= Name,

    };

}