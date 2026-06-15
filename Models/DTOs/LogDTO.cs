using System.ComponentModel.DataAnnotations;

namespace LuckyLogger.Models.DTOs;

public class LogDTO
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
}