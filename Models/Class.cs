using Aspschool.DTOs;

namespace Aspschool.Models;

public record Class
{
    public int Id { get; set; }
    public int ClassCapacity { get; set; }

    public ClassDTO asDto => new ClassDTO
    {
        Id= Id,
        ClassCapacity= ClassCapacity
    };

}