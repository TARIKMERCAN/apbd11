using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apbd11.Models;

public class Animal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(2000)]
    public string Description { get; set; }
    
    [Required]
    public int AnimalTypesId { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; set; }

    public AnimalTypes AnimalTypes { get; set; }
    public ICollection<Visit> Visits { get; set; }
}