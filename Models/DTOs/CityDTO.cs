using System.ComponentModel.DataAnnotations;

namespace LuckyLogger.Models.DTOs;

public class CityDTO
{
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; } 
}