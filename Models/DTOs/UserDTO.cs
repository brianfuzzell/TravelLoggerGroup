using System.ComponentModel.DataAnnotations;

namespace TravelLoggerGroup.Models.DTOs;

public class UserDTO
{
    public int Id { get; set; } 
    [Required]
    public string Email { get; set; } 
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}