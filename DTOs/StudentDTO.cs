using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aspschool.DTOs;

public record StudentDTO
{
    [JsonPropertyName("id")]
     public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("parent_mobile_num")]
    public long ParentMobileNum { get; set; }
    [JsonPropertyName("class_id")]
    public int ClassId { get; set; }

     [JsonPropertyName("teacher")]
    public List<TeacherDTO>Teacher { get; set; }

    [JsonPropertyName("subject")]
    public List<SubjectDTO>Subject { get; set; }



}

public record StudentCreateDTO
{

    [JsonPropertyName("name")]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("gender")]
    [Required]
    public string Gender { get; set; }
    [JsonPropertyName("address")]
    [Required]
    [MaxLength(255)]
    public string Address { get; set; }
    [JsonPropertyName("parent_mobile_num")]
    [Required]
    public long ParentMobileNum { get; set; }
    [JsonPropertyName("class_id")]
    [Required]
    public int ClassId { get; set; }

}


public record StudentUpdateDTO
{

    public string Name { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }


    [JsonPropertyName("address")]
    [Required]
    [MaxLength(255)]
    public string Address { get; set; }

    [JsonPropertyName("parent_mobile_num")]
    [Required]
    public long ParentMobileNum { get; set; }



}