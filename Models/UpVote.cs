using System.ComponentModel.DataAnnotations;

namespace TravelLoggerGroup.Models;

public class UpVote
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RecommendationId { get; set; }
}