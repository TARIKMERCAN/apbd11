using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apbd11.Models;

public class Visit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int EmployeeId { get; set; }
    
    [Required]
    public int AnimalId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Date { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; set; }

    public Employee Employee { get; set; }
    public Animal Animal { get; set; }
}