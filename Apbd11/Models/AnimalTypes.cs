using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apbd11.Models;

public class AnimalTypes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; set; }

    public ICollection<Animal> Animals { get; set; }
}