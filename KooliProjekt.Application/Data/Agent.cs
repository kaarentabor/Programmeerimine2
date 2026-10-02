
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data;

public class Agent
{
    [Key]
    public int AgentId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Role { get; set; } = string.Empty;
}
