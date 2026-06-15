using System.ComponentModel.DataAnnotations;

namespace LuckyLogger.Models;

public class City
{
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; } 
}