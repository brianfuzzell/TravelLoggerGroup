using System.ComponentModel.DataAnnotations;

namespace TravelLoggerGroup.Models.DTOs;

public class RecommendationDTO
{
    public int Id { get; set; } 
    public int CityId { get; set; } 
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}