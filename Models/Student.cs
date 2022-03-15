using Aspschool.DTOs;

namespace Aspschool.Models;

public record Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public long ParentMobileNum { get; set; }
    public int ClassId { get; set; }


    public StudentDTO asDto => new StudentDTO
    {
        Id = Id,
        Name = Name,
        DateOfBirth = DateOfBirth,
        Gender = Gender,
        Address = Address,
        ParentMobileNum = ParentMobileNum,
        ClassId = ClassId

    };


}