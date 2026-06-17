using System.ComponentModel.DataAnnotations;

namespace TravelLoggerGroup.Models.DTOs;

public class UpVoteDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RecommendationId { get; set; }
}