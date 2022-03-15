using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aspschool.DTOs;

public record ClassDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("class_capacity")]
    public int ClassCapacity { get; set; }

    [JsonPropertyName("students")]
    public List<StudentDTO> Student { get; set; }


}

public record ClassCreateDTO
{
    [JsonPropertyName("id")]
    [Required]
    public int Id { get; set; }

    [JsonPropertyName("class_capacity")]
    public int ClassCapacity { get; set; }

}


