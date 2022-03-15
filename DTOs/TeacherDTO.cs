using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aspschool.DTOs;

public record TeacherDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("subject_id")]
    public int SubjectId { get; set; }

    [JsonPropertyName("students_assigned")]
    public List<StudentDTO> Student { get; set; }


}

public record TeacherCreateDTO
{


    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    [Required]
    public string Gender { get; set; }

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("subject_id")]
    [Required]
    public int SubjectId { get; set; }

}


public record TeacherUpdateDTO
{

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("subject_id")]
    [Required]
    public int SubjectId { get; set; }

}


