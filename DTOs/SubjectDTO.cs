using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aspschool.DTOs;

public record SubjectDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

     [JsonPropertyName("teachers_asssigned")]
    public List<TeacherDTO> Teacher { get; set; }

}

public record SubjectCreateDTO
{
    [JsonPropertyName("id")]
    [Required]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }

}


