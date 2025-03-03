using System.ComponentModel.DataAnnotations;

public class Case
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CustomerName { get; set; }

    [Required]
    public string Query { get; set; }

    [Required]
    public string Channel { get; set; }  // e.g., WhatsApp, Email, AI, Calls

    [Required]
    public string Status { get; set; }  // Open, In Progress, Closed
}
