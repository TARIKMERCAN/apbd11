using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apbd11.Models;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [Required]
    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(200)]
    public string Email { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; set; }

    public ICollection<Visit> Visits { get; set; }
}